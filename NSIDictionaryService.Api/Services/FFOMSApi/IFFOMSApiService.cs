using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Services
{
    public interface IFFOMSApiService
    {
        public DictionaryDataDTO GetDictionaryData(string identifier);
        public DictionaryDataDTO GetDictionaryData(string identifier, string version);
        public DictionaryVersionDTO GetVersionData(string identifier);
        public FedPackDictionariesDTO GetFedPackDictVersions();
    }
}
