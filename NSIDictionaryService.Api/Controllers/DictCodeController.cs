using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NSIDictionaryService.Api.Repositories.Upload;

namespace NSIDictionaryService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DictCodeController: Controller
    {
        private readonly IDictCodeRepository _repository;

        public DictCodeController(IDictCodeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("getAllStoredDicts")]
        public IActionResult GetAll()
        {
            var result = _repository.GetAll();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _repository.GetByKeyAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // Other methods probably won't be necessary (at least for now)
    }
}
