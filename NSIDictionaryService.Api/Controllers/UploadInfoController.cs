using Microsoft.AspNetCore.Mvc;
using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Api.Repositories.Upload;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UploadInfoController : Controller
    {
        private readonly IUploadInfoRepository _repository;
        private readonly IDictVersionRepository _versionRepository;
        private readonly IDictCodeRepository _codeRepository;
        public UploadInfoController(
            IUploadInfoRepository repository, 
            IDictVersionRepository versionRepository, 
            IDictCodeRepository codeRepository)
        {
            _repository = repository;
            _versionRepository = versionRepository;
            _codeRepository = codeRepository;
        }

        [HttpGet("getAllUploads")]
        public IActionResult GetAll()
        {
            var result = _repository.GetAll();
            List<UploadInfoResponseDTO> responseDTOs = new List<UploadInfoResponseDTO>();
            foreach (var item in result) responseDTOs.Add(new UploadInfoResponseDTO(item));
            return Ok(responseDTOs);
        }

        [HttpGet("getUpload")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetByKeyAsync(id);
            if (result == null) return NotFound();
            var dtoResult = new UploadInfoResponseDTO(result);
            return Ok(dtoResult);
        }

        [HttpGet("getAllDictUploads")]
        public IActionResult GetByDict(string dictIdentifier)
        {
            var result = _repository.FindBy(x => x.DictCode == dictIdentifier);
            if (!result.Any())
            {
                return NotFound();
            }
            List<UploadInfoResponseDTO> responseDTOs = new List<UploadInfoResponseDTO>();
            foreach (var item in result) responseDTOs.Add(new UploadInfoResponseDTO(item));
            return Ok(responseDTOs);
        }

        [HttpPost("addUpload")]
        public IActionResult Post([FromBody] UploadInfoDTO value)
        {
            var version = _versionRepository.GetByKey(value.DictVersionId);
            if (version == null || version.IsDeleted) return BadRequest("Такой версии не существует");

            UploadInfo posted = new UploadInfo(
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
        public async Task<IActionResult> Put([FromBody] UploadInfoPutDTO value)
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
