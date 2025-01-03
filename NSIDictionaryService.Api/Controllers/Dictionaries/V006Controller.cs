﻿using Microsoft.AspNetCore.Mvc;
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
using NSIDictionaryService.Share.Helpers;
using NSIDictionaryService.Share.Exceptions;
using NSIDictionaryService.Api.Repositories.Upload;
using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NSIDictionaryService.Data.Models.Users;
using System.Security.Claims;
using System.Runtime.ConstrainedExecution;

namespace NSIDictionaryService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class V006Controller : Controller
    {
        private readonly int _dictionaryIdentifier;
        private readonly string _dictionaryIdentifierName = "V006";
        private readonly string _storagePath;
        private readonly string _outputPath;
        private readonly IV006Repository _dictRepository;
        private readonly V006Uploader _uploader;
        private readonly IFFOMSApiService _apiService;
        private readonly VersionHandler _versionHandler;
        private readonly IUploadInfoRepository _uploadRepository;
        private readonly IDictVersionRepository _versionRepository;
        private readonly ILogger<V006Controller> _logger;
        private readonly UniversalXMLDocCreator _xmlCreator;
        private readonly UserManager<User> _userManager;

        public V006Controller(
            IV006Repository dictRepository, 
            IDictPropertyRepository propertyRepository,
            IFFOMSApiService apiService,
            IDictVersionRepository versionRepository,
            IUploadInfoRepository uploadRepository,
            IWebHostEnvironment environment,
            IChangeRepository changeRepository,
            IDictCodeRepository codeRepository,
            ILogger<V006Controller> logger,
            ILogger<V006Uploader> uploadLogger,
            UserManager<User> userManager)
        {
            _uploader = new V006Uploader(propertyRepository, dictRepository, uploadLogger, changeRepository);
            _apiService = apiService;
            _dictRepository = dictRepository;
            _versionRepository = versionRepository;
            _versionHandler = new VersionHandler(versionRepository);
            _uploadRepository = uploadRepository;
            _userManager = userManager;
            _storagePath = Path.Combine(environment.ContentRootPath, "Uploads");
            _outputPath = Path.Combine(environment.ContentRootPath, "Reports");

            _xmlCreator = new UniversalXMLDocCreator(propertyRepository);

            var name = codeRepository.First(x => x.Name == _dictionaryIdentifierName);
            if (name == null) throw new Exception("Нет записи о справочнике V006 в таблице названий справочников");
            _dictionaryIdentifier = name.Id;

            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
            if (!Directory.Exists(_outputPath))
            {
                Directory.CreateDirectory(_outputPath);
            }

            _logger = logger;
        }

        [HttpGet("getAllEntries")]
        public IActionResult GetAll()
        {
            var result = _dictRepository.GetAll();
            List<V006ResponseDTO> dtos = new List<V006ResponseDTO>();
            foreach (var item in result) dtos.Add(UniversalResponseMapper.ConvertToResponse(item));
            return Ok(dtos);
        }

        [HttpGet("getEntryById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dictRepository.GetByKeyAsync(id);
            if (result == null) return NotFound();
            return Ok(UniversalResponseMapper.ConvertToResponse(result));
        }

        [HttpGet("getEntryByCode")]
        public IActionResult GetByCode(int code)
        {
            var result = _dictRepository.FindBy(x => x.Code == code && !x.IsDeleted);
            if (result == null) return NotFound();
            List<V006ResponseDTO> dtos = new List<V006ResponseDTO>();
            foreach (var item in result) dtos.Add(UniversalResponseMapper.ConvertToResponse(item));
            return Ok(dtos);
        }

        [HttpPost("addEntry")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> PostAsync([FromBody] V006DTO value)
        {
            var version = await _versionRepository.GetByKeyAsync(value.DictVersionId);
            if (version == null || version.DictCodeId != _dictionaryIdentifier || version.IsDeleted)
                return BadRequest("Неверная версия словаря");

            //if (value.EndDate < DateTime.UtcNow || value.EndDate < value.BeginDate) 
            //    return BadRequest("Запись больше не действительна");

            V006Dictionary posted = V006DTOtoEntityMapper.Convert(value);
            _dictRepository.Add(posted);
            await _dictRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("changeEntry")]
        [Authorize(Roles = "Admin")]
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
                    if (version == null || version.DictCodeId != _dictionaryIdentifier || version.IsDeleted)
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
                _logger.LogError($"Ошибка при попытке изменения словаря: {ex.Message}");
                return BadRequest($"Неверный формат: {ex.Message}");
            }
        }

        [HttpDelete("deleteEntry/{id}")]
        [Authorize(Roles = "Admin")]
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
            int userId;
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
            {
                userId = 0;
            }
            UploadInfo uploadFile = new UploadInfo()
            {
                UploadingUserId = userId,
                UploadDate = DateTime.Now,
                DictCode = _dictionaryIdentifierName,
                UploadMethodId = 2, // TODO : Change this when you'll add proper codes
                UploadResultId = 1
            };

            var versionDto = _apiService.GetVersionData(_dictionaryIdentifierName).VersionData.FirstOrDefault();
            if (versionDto is null) throw new FailedAccessToExternalServiceException("Ошибка доступа к сервису NSI FFOMS");

            try
            {
                DictVersion version = await _versionHandler.HandleVersion(versionDto, _dictionaryIdentifier);
                uploadFile.DictVersionId = version.Id;

                _uploadRepository.Add(uploadFile);
                await _uploadRepository.SaveChangesAsync();

                var data = _apiService.GetDictionaryData(_dictionaryIdentifierName);
                if (data is null) throw new FailedAccessToExternalServiceException("Ошибка доступа к сервису NSI FFOMS");

                bool result = await _uploader.UploadFromJson(data, version, uploadFile.Id);
                if (!result)
                {
                    await SetErrorStatusAsync(uploadFile, "Ошибка при заполнении словаря");
                    _logger.LogError("Ошибка при заполнении словаря V006 из API");
                    return BadRequest("Ошибка при заполнении словаря");
                }

                _uploadRepository.Edit(uploadFile);
                await _uploadRepository.SaveChangesAsync();

                _logger.LogInformation("Успешно загружен словарь V006 из API");
                return Ok();
            }
            catch(InvalidVersionException ex)
            {
                uploadFile.DictVersionId = ex.Version.Id;
                await SetErrorStatusAsync(uploadFile, ex.Message);
                _logger.LogError($"Ошибка при загрузке словаря V006 из API: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (FailedAccessToExternalServiceException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message); // This should be internal server error
            }
        }

        [HttpPost("AddFromXML")]
        public async Task<IActionResult> UploadFromXML(IFormFile formFile)
        {
            int userId;
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
            {
                userId = 0;
            }
            UploadInfo uploadFile = new UploadInfo()
            {
                UploadingUserId = userId,
                UploadDate = DateTime.Now,
                DictCode = _dictionaryIdentifierName,
                UploadMethodId = 2, // TODO : Change this when you'll add proper codes
                UploadResultId = 1
            };

            if (!formFile.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Отправленный файл не является XML файлом.");
            }

            try
            {
                // Give unique names to files
                string fileName = formFile.FileName.Replace( 
                    Path.GetFileNameWithoutExtension(formFile.FileName),
                    string.Concat(Path.GetFileNameWithoutExtension(formFile.FileName), 
                        DateTime.Now.ToString("yyyyMMddHHmmssffff")));

                var filePath = Path.Combine(_storagePath, Path.GetFileName(fileName));
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
                uploadFile.DictCode = filePath; // For API uploads, this just has dictionary name

                XDocument XMLData;

                
                using (var reader = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
                {
                    XMLData = XDocument.Load(reader);
                }

                // Validation of XML here
                SimpleXMLValidator.Validate(XMLData); //This is too simple, prob should rework it

                // Because why change what works?
                var versionDto = XMLPackageToDictDataMapper.GetVersion(XMLData); 

                DictVersion version = await _versionHandler.HandleVersion(versionDto, _dictionaryIdentifier);
                uploadFile.DictVersionId = version.Id;

                _uploadRepository.Add(uploadFile);
                await _uploadRepository.SaveChangesAsync();

                var data = XMLPackageToDictDataMapper.GetData(XMLData);

                bool result = await _uploader.UploadFromJson(data, version, uploadFile.Id);
                if (!result)
                {
                    await SetErrorStatusAsync(uploadFile, "Ошибка при заполнении словаря");
                    return BadRequest("Ошибка при заполнении словаря");
                }

                _uploadRepository.Edit(uploadFile);
                await _uploadRepository.SaveChangesAsync();

                _logger.LogInformation("Успешно загружен словарь V006 из XML-файла");
                return Ok();
            }
            catch (InvalidVersionException ex)
            {
                uploadFile.DictVersionId = ex.Version.Id;
                await SetErrorStatusAsync(uploadFile, ex.Message);
                _logger.LogError($"Ошибка при загрузке словаря V006 из XML: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (XMLValidationException ex)
            {
                _logger.LogError($"Ошибка при загрузке словаря V006 из XML: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (XmlException ex)
            {
                _logger.LogError("Ошибка при загрузке словаря V006 из XML: неверный формат XML");
                return BadRequest("Ошибка при загрузке словаря V006 из XML: неверный формат XML");
            }
        }

        [HttpGet("downloadXML")]
        [AllowAnonymous]
        public async Task<IActionResult> getXML()
        {
            DictVersion version = await _versionRepository.FirstAsync(x => x.DictCodeId == _dictionaryIdentifier && !x.IsDeleted);
            if (version == null) return BadRequest($"Записи словаря {_dictionaryIdentifierName} отсутствуют");

            var data = _dictRepository.GetAll().Cast<BaseDictionaryType<int>>().ToList();

            var xdoc = _xmlCreator.CreateDocument<int>(data, version, _dictionaryIdentifierName);

            string fileName = Path.Combine(_outputPath,
                $"{_dictionaryIdentifierName}_" +
                $"{DateTime.Now.ToString("yyyyMMddHHmmssffff")}.xml");

            xdoc.Save(fileName);

            //StreamReader file = new StreamReader(fileName, Encoding.GetEncoding("windows-1251"));

            //Response.Headers.Append("Content-Disposition", $"attachment; filename={_dictionaryIdentifierName}");

            return PhysicalFile(fileName, "text/xml; charset=windows-1251");
        }

        private async Task SetErrorStatusAsync(UploadInfo uploadFile, string message)
        {
            uploadFile.UploadResultId = 2;
            uploadFile.ErrorDescription = message;
            if (uploadFile.Id != 0) _uploadRepository.Edit(uploadFile);
            else _uploadRepository.Add(uploadFile);
            await _uploadRepository.SaveChangesAsync();
        }
    }
}
