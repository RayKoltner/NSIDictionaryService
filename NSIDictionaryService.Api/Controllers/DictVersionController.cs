using Microsoft.AspNetCore.Mvc;
using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Api.Services.Mappers;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DictVersionController : Controller
    {
        private readonly IDictVersionRepository _repository;
        public DictVersionController(IDictVersionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("getAllVersions")]
        public IActionResult GetAll()
        {
            var result = _repository.GetAll();
            return Ok(result);
        }

        [HttpGet("getVersion")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetByKeyAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("addVersion")]
        public async Task<IActionResult> PostAsync([FromBody] DictVersionDTO value)
        {
            try
            {
                DictVersion posted = DictVersionDTOtoEntityMapper.Convert(value);
                var existing = await _repository.FirstAsync(x => x.DictionaryCode == posted.DictionaryCode & !x.IsDeleted);
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

        [HttpDelete("deleteVersion")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByKeyAsync(id);
            if (existing is null) return NotFound();
            await _repository.VirtualDelete(existing, 0); // TODO : Change this when you'll add users
            return Ok();
        }
    }
}