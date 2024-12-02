using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs;
using System.Reflection;

namespace NSIDictionaryService.Api.Services
{
    public class DictionaryUploader
    {
        private readonly IRepository<DictProperty> _propertyRepository;
        public DictionaryUploader(IRepository<DictProperty> dictPropertyRepository)
        {
            _propertyRepository = dictPropertyRepository;
        }
        public object? Upload(DictionaryDataDTO dictionaryData, DictVersion version)
        {
            try
            {
                Type dictType = Type.GetType(version.DictionaryCode);
                object dictionary = dictType.GetConstructor(Type.EmptyTypes).Invoke(null);
                Convert.ChangeType(dictionary, dictType);

                List<DictProperty> properties = _propertyRepository.FindBy(
                    x => x.DictionaryName == version.DictionaryCode).ToList();

                UniversalDictionaryMapper.FillWithData(dictionaryData.ResponseData.First(), dictionary, properties);

                return dictionary;
            }
            catch
            {
                return null;
            }
        }
    }
}
