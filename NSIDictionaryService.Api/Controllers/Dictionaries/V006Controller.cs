using Microsoft.AspNetCore.Mvc;
using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Api.Repositories.Dictionaries;
using NSIDictionaryService.Api.Services;
using NSIDictionaryService.Api.Services.Mappers;
using NSIDictionaryService.Api.Services.UploadDictionary;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs;
using NSIDictionaryService.Share.DTOs.V006DTOs;
using NSIDictionaryService.Api.Services.Handlers;
using NSIDictionaryService.Api.Exceptions;
using System.Xml.Linq;
using System.Text;

namespace NSIDictionaryService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class V006Controller : Controller
    {
        private readonly string _dictionaryIdentifier = "V006";
        private readonly string _storagePath;
        private readonly IV006Repository _dictRepository;
        private readonly V006Uploader _uploader;
        private readonly IFFOMSApiService _apiService;
        private readonly VersionHandler _versionHandler;
        private readonly IUploadInfoRepository _uploadRepository;
        private readonly IDictVersionRepository _versionRepository;

        public V006Controller(
            IV006Repository dictRepository, 
            IDictPropertyRepository propertyRepository,
            IFFOMSApiService apiService,
            IDictVersionRepository versionRepository,
            IUploadInfoRepository uploadRepository,
            IWebHostEnvironment environment)
        {
            _uploader = new V006Uploader(propertyRepository, dictRepository);
            _apiService = apiService;
            _dictRepository = dictRepository;
            _versionRepository = versionRepository;
            _versionHandler = new VersionHandler(versionRepository);
            _uploadRepository = uploadRepository;
            _storagePath = Path.Combine(environment.ContentRootPath, "Uploads");

            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        [HttpGet("getAllEntries")]
        public IActionResult GetAll()
        {
            var result = _dictRepository.GetAll();
            List<V006ResponseDTO> dtos = new List<V006ResponseDTO>();
            foreach (var item in result) dtos.Add(new V006ResponseDTO(item));
            return Ok(dtos);
        }

        [HttpGet("getEntryById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dictRepository.GetByKeyAsync(id);
            if (result == null) return NotFound();
            return Ok(new V006ResponseDTO(result));
        }

        [HttpGet("getEntryByCode")]
        public IActionResult GetByCode(int code)
        {
            var result = _dictRepository.FindBy(x => x.Code == code && !x.IsDeleted);
            if (result == null) return NotFound();
            List<V006ResponseDTO> dtos = new List<V006ResponseDTO>();
            foreach (var item in result) dtos.Add(new V006ResponseDTO(item));
            return Ok(dtos);
        }

        [HttpPost("addEntry")]
        public async Task<IActionResult> PostAsync([FromBody] V006DTO value)
        {
            var version = await _versionRepository.GetByKeyAsync(value.DictVersionId);
            if (version == null || !version.DictionaryCode.Equals(_dictionaryIdentifier) || version.IsDeleted)
                return BadRequest("Неверная версия словаря");

            if (value.EndDate < DateTime.UtcNow || value.EndDate < value.BeginDate) 
                return BadRequest("Запись больше не действительна");

            V006Dictionary posted = V006DTOtoEntityMapper.Convert(value);
            _dictRepository.Add(posted);
            await _dictRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("changeEntry")]
        public async Task<IActionResult> Put([FromBody] V006PutDTO value)
        {
            try
            {
                var existing = await _dictRepository.GetByKeyAsync(value.Id);
                if (existing is null) return NotFound();
                if (existing.IsDeleted) return BadRequest("Попытка изменения удалённой записи");
                if (existing.DictVersionId != value.DictVersionId)
                {
                    var version = _versionRepository.GetByKey(value.DictVersionId);
                    if (version == null || !version.DictionaryCode.Equals(_dictionaryIdentifier) || version.IsDeleted)
                        return BadRequest("Неверная версия словаря");
                }
                
                existing.Code = value.Code;
                existing.BeginDate = value.BeginDate;
                existing.EndDate = value.EndDate;
                existing.Name = value.Name;
                existing.Comments = value.Comments;
                existing.DictVersionId = value.DictVersionId;

                _dictRepository.Edit(existing);
                await _dictRepository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Неверный формат: {ex.Message}");
            }
        }

        [HttpDelete("deleteEntry")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _dictRepository.GetByKeyAsync(id);
            if (existing is null) return NotFound();
            await _dictRepository.VirtualDelete(existing, 0); // TODO : Change this when you'll add users
            return Ok();
        }

        [HttpPost("AddFromApi")]
        public async Task<IActionResult> UploadFromApi()
        {
            UploadInfo uploadFile = new UploadInfo()
            {
                UploadingUserId = 0, // TODO : Change this when you'll add users
                UploadDate = DateTime.Now,
                DictCode = _dictionaryIdentifier,
                UploadMethodId = 2, // TODO : Change this when you'll add proper codes
                UploadResultId = 1
            };

            var versionDto = _apiService.GetVersionData(_dictionaryIdentifier).VersionData.FirstOrDefault();
            if (versionDto is null) throw new FailedAccessToExternalServiceException("Ошибка доступа к сервису NSI FFOMS");

            try
            {
                DictVersion version = await _versionHandler.HandleVersion(versionDto, _dictionaryIdentifier);
                uploadFile.DictVersionId = version.Id;

                var data = _apiService.GetDictionaryData(_dictionaryIdentifier);
                if (data is null) throw new FailedAccessToExternalServiceException("Ошибка доступа к сервису NSI FFOMS");

                bool result = await _uploader.UploadFromJson(data, version);
                if (!result)
                {
                    await SetErrorStatusAsync(uploadFile, "Ошибка при заполнении словаря");
                    return BadRequest("Ошибка при заполнении словаря");
                }

                _uploadRepository.Add(uploadFile);
                await _uploadRepository.SaveChangesAsync();

                return Ok();
            }
            catch(InvalidVersionException ex)
            {
                uploadFile.DictVersionId = ex.Version.Id;
                await SetErrorStatusAsync(uploadFile, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (FailedAccessToExternalServiceException ex)
            {
                return BadRequest(ex.Message); // This should be internal server error
            }
        }

        [HttpPost("AddFromXML")]
        public async Task<IActionResult> UploadFromXML(IFormFile formFile)
        {
            UploadInfo uploadFile = new UploadInfo()
            {
                UploadingUserId = 0, // TODO : Change this when you'll add users
                UploadDate = DateTime.Now,
                DictCode = _dictionaryIdentifier,
                UploadMethodId = 3, // TODO : Change this when you'll add proper codes
                UploadResultId = 1
            };

            if (formFile == null || formFile.Length == 0)
            {
                return BadRequest("Запрос не содержит файла.");
            }

            // Check for XML extension if needed
            if (!formFile.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Отправленный файл не является XML файлом.");
            }

            try
            {
                string fileName = formFile.FileName.Replace(
                    Path.GetFileNameWithoutExtension(formFile.FileName),
                    string.Concat(Path.GetFileNameWithoutExtension(formFile.FileName), 
                        DateTime.Now.ToString("yyyyMMddHHmmssffff")));

                var filePath = Path.Combine(_storagePath, Path.GetFileName(fileName));
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
                uploadFile.DictCode = filePath;

                XDocument XMLData;

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (var reader = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
                {
                    XMLData = XDocument.Load(reader);
                }

                // Validation of XML here

                var versionDto = XMLPackageToDictDataMapper.GetVersion(XMLData, _dictionaryIdentifier);

                DictVersion version = await _versionHandler.HandleVersion(versionDto, _dictionaryIdentifier);
                uploadFile.DictVersionId = version.Id;

                var data = XMLPackageToDictDataMapper.GetData(XMLData);

                bool result = await _uploader.UploadFromJson(data, version);
                if (!result)
                {
                    await SetErrorStatusAsync(uploadFile, "Ошибка при заполнении словаря");
                    return BadRequest("Ошибка при заполнении словаря");
                }

                _uploadRepository.Add(uploadFile);
                await _uploadRepository.SaveChangesAsync();

                return Ok();
            }
            catch (InvalidVersionException ex)
            {
                uploadFile.DictVersionId = ex.Version.Id;
                await SetErrorStatusAsync(uploadFile, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        private async Task SetErrorStatusAsync(UploadInfo uploadFile, string message)
        {
            uploadFile.UploadResultId = 2;
            uploadFile.ErrorDescription = message;
            _uploadRepository.Add(uploadFile);
            await _uploadRepository.SaveChangesAsync();
        }
    }
}
