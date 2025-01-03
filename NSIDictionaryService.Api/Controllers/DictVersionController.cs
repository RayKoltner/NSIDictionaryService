﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Api.Repositories.Upload;
using NSIDictionaryService.Api.Services.Mappers;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class DictVersionController : Controller
    {
        private readonly IDictVersionRepository _repository;
        private readonly IDictCodeRepository _codeRepository;
        public DictVersionController(
            IDictVersionRepository repository,
            IDictCodeRepository codeRepository)
        {
            _repository = repository;
            _codeRepository = codeRepository;
        }

        [HttpGet("getAllVersions")]
        public async Task<IActionResult> GetAll()
        {
            var result = _repository.GetAll();
            var codes = _codeRepository.GetAll().ToList();
            List<DictVersionResponseDTO> response = new List<DictVersionResponseDTO>();
            foreach (var item in result)
            {
                var code = codes.FirstOrDefault(x => x.Id == item.DictCodeId);
                response.Add(UniversalResponseMapper.ConvertToResponse(item, code));
            }
            return Ok(response);
        }

        [HttpGet("getVersion")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetByKeyAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("getCurrentVersion/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            name = name.ToUpper();
            var code = _codeRepository.First(x => x.Name == name);
            if (code == null) return NotFound("Нет справочника с таким названием");
            var version = _repository.First(x => x.DictCodeId == code.Id && !x.IsDeleted);
            if (version == null) return NotFound("Нет версий этого справочника");
            return Ok(UniversalResponseMapper.ConvertToResponse(version, code));
        }

        [HttpPost("addVersion")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostAsync([FromBody] DictVersionDTO value)
        {
            try
            {
                DictVersion posted = DictVersionDTOtoEntityMapper.Convert(value);

                var storedName = _codeRepository.GetByKey(posted.DictCodeId);
                if (storedName is null) return BadRequest($"Нет справочника с таким id названия: {posted.DictCodeId}");

                var existing = await _repository.FirstAsync(x => x.DictCodeId == posted.DictCodeId & !x.IsDeleted);
                if (existing != null)
                {
                    if (existing.VersionCode >= posted.VersionCode)
                        return BadRequest($"Такая или большая версия уже существует: {posted.VersionCode}");
                    await _repository.VirtualDelete(existing, 0); // TODO : Change this when you'll add users
                }
                _repository.Add(posted);
                await _repository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Неверный формат: {ex.Message}");
            }
        }

        [HttpPut("changeVersion")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] DictVersionPutDTO value)
        {
            try
            {
                var existing = await _repository.GetByKeyAsync(value.Id);
                if (existing is null) return NotFound();
                if (existing.IsDeleted) return BadRequest("Попытка изменения удалённой записи");
                if (existing.VersionCode > value.VersionCode) return BadRequest("Нельзя уменьшать номер версии");

                existing.VersionCode = value.VersionCode;
                existing.PublicationDate = DateTime.Parse(value.PublicationDate);

                _repository.Edit(existing);
                await _repository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Неверный формат: {ex.Message}");
            }
        }

        [HttpDelete("deleteVersion/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByKeyAsync(id);
            if (existing is null) return NotFound();
            await _repository.VirtualDelete(existing, 0); // TODO : Change this when you'll add users
            return Ok();
        }
    }
}