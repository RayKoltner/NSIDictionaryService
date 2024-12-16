using Microsoft.AspNetCore.Mvc;
using NSIDictionaryService.Api.Services;

namespace NSIDictionaryService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FFOMSApiController : Controller
    {
        private readonly IFFOMSApiService _apiService;
        public FFOMSApiController(IFFOMSApiService apiService)
        {
            _apiService = apiService;
        }
        [HttpGet("getDictionary")]
        public IActionResult GetDictionary(string identifier)
        {
            var result = _apiService.GetDictionaryData(identifier);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("getDictionaryByVersion")]
        public IActionResult GetDictionary(string identifier, string version)
        {
            var result = _apiService.GetDictionaryData(identifier, version);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("getVersions")]
        public IActionResult GetVersions(string identifier)
        {
            var result = _apiService.GetVersionData(identifier);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        //[HttpGet("getFedPack")]
        //public IActionResult GetFedPack()
        //{
        //    var result = _apiService.GetFedPackDictVersions();
        //    if (result == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(result);
        //}
    }
}
