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
using System;
using System.Globalization;

namespace NSIDictionaryService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class V006Controller : Controller
    {
        private readonly IV006Repository _dictRepository;
        private readonly V006Uploader _uploader;
        private readonly IFFOMSApiService _apiService;
        private readonly IDictVersionRepository _versionRepository;
        private readonly IUploadDictRepository _uploadRepository;

        public V006Controller(
            IV006Repository dictRepository, 
            IDictPropertyRepository propertyRepository,
            IFFOMSApiService apiService,
            IDictVersionRepository versionRepository,
            IUploadDictRepository uploadRepository)
        {
            _uploader = new V006Uploader(propertyRepository, dictRepository);
            _apiService = apiService;
            _dictRepository = dictRepository;
            _versionRepository = versionRepository;
            _uploadRepository = uploadRepository;
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
            if (version == null || !version.DictionaryCode.Equals("V006") || version.IsDeleted)
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
                    if (version == null || !version.DictionaryCode.Equals("V006") || version.IsDeleted)
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
            UploadDict uploadFile = new UploadDict()
            {
                UploadingUserId = 0, // TODO : Change this when you'll add users
                UploadDate = DateTime.Now,
                DictCode = "V006",
                UploadMethodId = 2, // TODO : Change this when you'll add proper codes
                UploadResultId = 1 
            };

            var versionDto = _apiService.GetVersionData("V006").VersionData.FirstOrDefault();
            if (versionDto is null)
            {
                SetErrorStatusAsync(uploadFile, "Ошибка доступа к сервису NSI FFOMS");
                return BadRequest("Ошибка доступа к сервису NSI FFOMS");
            }

            DictVersion version;
            var existingVersion = await _versionRepository.FirstAsync(
                x => x.DictionaryCode == "V006" && !x.IsDeleted);

            if (existingVersion != null && decimal.Parse(versionDto.Version, CultureInfo.InvariantCulture) <= existingVersion.VersionCode)
            {
                uploadFile.DictVersionId = existingVersion.Id; // TODO : Change this when you'll add proper codes
                SetErrorStatusAsync(uploadFile, "Справочник такой или более новой версии уже загружен");
                return BadRequest("Справочник такой или более новой версии уже загружен");
            }
            else version = new DictVersion()
            {
                DictionaryCode = "V006",
                VersionCode = decimal.Parse(versionDto.Version, CultureInfo.InvariantCulture),
                PublicationDate = versionDto.UpdateDate.ToDateTime(TimeOnly.MinValue)
            };
            if (existingVersion != null) await _versionRepository.VirtualDelete(existingVersion, 0); // TODO : Change this when you'll add users
            _versionRepository.Add(version);
            await _versionRepository.SaveChangesAsync();
            version = await _versionRepository.FirstAsync(x => x.DictionaryCode == "V006" && !x.IsDeleted);

            var data = _apiService.GetDictionaryData("V006");
            if (data == null)
            {
                SetErrorStatusAsync(uploadFile, "Ошибка доступа к сервису NSI FFOMS");
                return BadRequest("Ошибка доступа к сервису NSI FFOMS");
            }

            try
            {
                bool result = _uploader.UploadFromJson(data, version);
                if (!result)
                {
                    SetErrorStatusAsync(uploadFile, "Ошибка при заполнении словаря");
                    return BadRequest("Ошибка при заполнении словаря");
                }
                _uploadRepository.Add(uploadFile);
                await _uploadRepository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                SetErrorStatusAsync(uploadFile, ex.Message);
                return BadRequest(ex);
            }
        }
        [HttpGet("TestHowDataIsFilled")]
        public async Task<IActionResult> MapFromApiTest()
        {
            var data = _apiService.GetDictionaryData("V006");
            if (data == null)
            {
                return BadRequest("Ошибка доступа к сервису NSI FFOMS");
            }

            DictVersion version = new DictVersion()
            {
                DictionaryCode = "V006"
            };

            var result = _uploader.ConvertJsonToModel(data, version);

            return Ok(result);
        }

        private async Task SetErrorStatusAsync(UploadDict uploadFile, string message)
        {
            uploadFile.UploadResultId = 2;
            uploadFile.ErrorDescription = message;
            _uploadRepository.Add(uploadFile);
            await _uploadRepository.SaveChangesAsync();
        }
    }
}
