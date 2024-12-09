using Microsoft.AspNetCore.Mvc;
using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Api.Services.Mappers;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DictPropertyController: Controller
    {
        private readonly IDictPropertyRepository _repository;
        public DictPropertyController(IDictPropertyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("getAllProperties")]
        public IActionResult GetAll()
        {
            var result = _repository.GetAll();
            return Ok(result);
        }

        [HttpGet("getProperty")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetByKeyAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("getAllDictProperties")]
        public IActionResult GetByDict(string dictIdentifier)
        {
            var result = _repository.FindBy(x => x.DictionaryName == dictIdentifier);
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("addProperty")]
        public IActionResult Post([FromBody] DictPropertyDTO value)
        {
            DictProperty posted = DictPropertyDTOtoEntityMapper.Convert(value);
            _repository.Add(posted);
            _repository.Save();
            return Ok();
        }

        [HttpPut("changeProperty")]
        public async Task<IActionResult> Put([FromBody] DictPropertyPutDTO value)
        {
            var existing = await _repository.GetByKeyAsync(value.Id);
            if (existing is null) return NotFound();

            existing.DictionaryName = value.DictionaryName;
            existing.PropertyCode = value.PropertyCode;
            existing.PropertyName = value.PropertyName;

            _repository.Edit(existing);
            await _repository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("deleteProperty")]
        public async Task<IActionResult> Delete(int  id)
        {
            var existing = await _repository.GetByKeyAsync(id);
            if (existing is null) return NotFound();
            await _repository.VirtualDelete(existing, 0); // TODO : Change this when you'll add users
            return Ok();
        }
    }
}
