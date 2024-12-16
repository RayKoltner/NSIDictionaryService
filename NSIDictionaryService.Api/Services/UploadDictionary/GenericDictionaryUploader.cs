using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Services
{
    public abstract class GenericDictionaryUploader<T, P> where T : BaseDictionaryType<P>
    {
        private readonly IRepository<DictProperty> _propertyRepository;
        public GenericDictionaryUploader(IRepository<DictProperty> dictPropertyRepository)
        {
            _propertyRepository = dictPropertyRepository;
        }
        public List<T> FillWithData(DictionaryDataDTO dictionaryData, List<T> emptyDicts, DictVersion version)
        {
            List<T> result = new List<T>();
            List<DictProperty> dictProperties = [.. _propertyRepository.FindBy(x => x.DictCodeId == version.DictCodeId)];
            for (int i = 0; i < emptyDicts.Count; i++)
            {
                result.Add((T)UniversalDictionaryMapper.FillWithData<P>(
                    dictionaryData.ResponseData[i],
                    emptyDicts[i],
                    dictProperties));
            }
            
            return result;
        }
        public abstract List<T> ConvertJsonToModel(DictionaryDataDTO dictionaryData, DictVersion version);
    }
}
