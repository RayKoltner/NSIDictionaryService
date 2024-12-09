using Microsoft.AspNetCore.Mvc;
using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UploadDictController : Controller
    {
        private readonly IUploadDictRepository _repository;
        private readonly IDictVersionRepository _versionRepository;
        public UploadDictController(IUploadDictRepository repository, IDictVersionRepository versionRepository)
        {
            _repository = repository;
            _versionRepository = versionRepository;
        }

        [HttpGet("getAllUploads")]
        public IActionResult GetAll()
        {
            var result = _repository.GetAll();
            return Ok(result);
        }

        [HttpGet("getUpload")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetByKeyAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("getAllDictUploads")]
        public IActionResult GetByDict(string dictIdentifier)
        {
            var result = _repository.FindBy(x => x.DictCode == dictIdentifier);
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("addUpload")]
        public IActionResult Post([FromBody] UploadDictDTO value)
        {
            var version = _versionRepository.GetByKey(value.DictVersionId);
            if (version == null || version.IsDeleted) return BadRequest("Такой версии не существует");

            UploadDict posted = new UploadDict(
                0, //TODO : Add actual codes for user, method and result
                DateTime.Now,
                value.DictCode,
                value.DictVersionId,
                1,
                1
                );

            _repository.Add(posted);
            _repository.Save();
            return Ok();
        }

        [HttpPut("changeUpload")]
        public async Task<IActionResult> Put([FromBody] UploadDictPutDTO value)
        {
            var existing = await _repository.GetByKeyAsync(value.Id);
            if (existing is null) return NotFound();

            var version = _versionRepository.GetByKey(value.DictVersionId);
            if (version == null || version.IsDeleted) return BadRequest("Такой версии не существует");

            existing.DictCode = value.DictCode;
            existing.DictVersionId = value.DictVersionId;

            _repository.Edit(existing);
            await _repository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("deleteUpload")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByKeyAsync(id);
            if (existing is null) return NotFound();
            await _repository.VirtualDelete(existing, 0); // TODO : Change this when you'll add users
            return Ok();
        }
    }
}
