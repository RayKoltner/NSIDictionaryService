using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Services.UploadDictionary
{
    public class V006Uploader : GenericDictionaryUploader<V006Dictionary>
    {
        public V006Uploader(IDictPropertyRepository dictPropertyRepository) : base(dictPropertyRepository)
        {
        }

        public override List<V006Dictionary> ConvertToModel(DictionaryDataDTO dictionaryData, DictVersion version)
        {
            List<V006Dictionary> emptyDicts = new List<V006Dictionary>();
            for (int i = 0; i < dictionaryData.ResponseData.Count; i++)
            {
                emptyDicts.Add(new V006Dictionary());
            }

            var result = base.FillWithData(dictionaryData, emptyDicts, version);
            return result;
        }
    }
}
