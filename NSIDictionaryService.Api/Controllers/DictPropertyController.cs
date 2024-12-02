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
        private readonly IRepository<DictProperty> _repository;
        public DictPropertyController(IRepository<DictProperty> repository)
        {
            _repository = repository;
        }

        [HttpGet("getAllProperties")]
        public IActionResult GetAll()
        {
            var result = _repository.GetAll();
            return Ok(result);
        }

        [HttpGet("getDictProperties")]
        public IActionResult Get(string identifier)
        {
            var result = _repository.FindBy(x => x.DictionaryName == identifier);
            if (result is  null)
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
        public IActionResult Put([FromBody] DictPropertyPutDTO value)
        {
            var existing = _repository.GetByKey(value.Id);
            if (existing is null) return NotFound();

            existing.DictionaryName = value.DictionaryName;
            existing.PropertyCode = value.PropertyCode;
            existing.PropertyName = value.PropertyName;

            _repository.Edit(existing);
            _repository.Save();
            return Ok();
        }

        [HttpDelete("deleteProperty")]
        public IActionResult Delete(int  id)
        {
            var existing = _repository.GetByKey(id);
            if (existing is null) return NotFound();
            _repository.VirtualDelete(existing, 0); // TODO : Change this when you'll add users
            return Ok();
        }
    }
}
