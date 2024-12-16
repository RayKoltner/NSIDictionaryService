using NSIDictionaryService.Api.Exceptions;
using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Api.Repositories.Upload;
using NSIDictionaryService.Data.Models;
using System.Globalization;

namespace NSIDictionaryService.Api.Services.VersionChecker
{
    public class VersionChecker
    {
        private readonly IFFOMSApiService _apiService;
        private readonly IDictVersionRepository _versionRepository;
        private readonly IDictCodeRepository _codeRepository;

        public VersionChecker(
            IFFOMSApiService apiService, 
            IDictVersionRepository versionRepository, 
            IDictCodeRepository codeRepository)
        {
            _apiService = apiService;
            _versionRepository = versionRepository;
            _codeRepository = codeRepository;
        }

        public void Check(List<DictDependancy> dependancies)
        {
            List<string> errors = new List<string>();
            foreach (DictDependancy dependency in dependancies)
            {
                var dict = _codeRepository.GetByKey(dependency.DependancyId);

                if (dict is null) throw new Exception("Такого словаря нет в базе");
                string dictName = dict.Name;

                var storedVersion = _versionRepository.First(x => x.DictCodeId == dependency.DependancyId && !x.IsDeleted);

                if (storedVersion is null)
                {
                    errors.Add(dictName);
                    continue;
                }

                var currentVersion = _apiService.GetVersionData(dictName).VersionData.FirstOrDefault().Version;

                if(decimal.Parse(currentVersion, CultureInfo.InvariantCulture) > storedVersion.VersionCode)
                {
                    errors.Add(dictName);
                }
            }
            if (errors.Count > 0)
            {
                string errorString = string.Join(", ", errors); 
                throw new OutdatedDependancyException($"Сначала обновите следующие словари: {errorString}");
            }
        }
    }
}
