using Microsoft.AspNetCore.Mvc;
using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Api.Services;
using NSIDictionaryService.Api.Services.UploadDictionary;
using NSIDictionaryService.Data.Models.Dictionaries;

namespace NSIDictionaryService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class V006Controller: Controller
    {
        //private readonly IRepository<V006Dictionary> _dictRepository;
        private readonly V006Uploader _uploader;
        private readonly IFFOMSApiService _apiService;

        //public V006Controller(IRepository<V006Dictionary> dictRepository)
        //{
        //    _dictRepository = dictRepository;
        //}

        public V006Controller(IDictPropertyRepository propertyRepository, IFFOMSApiService apiService)
        {
            _uploader = new V006Uploader(propertyRepository);
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult getLatestDictionary()
        {

        }
    }
}
