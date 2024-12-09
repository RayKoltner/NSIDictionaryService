using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Api.Repositories.Dictionaries;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Services.UploadDictionary
{
    public class V006Uploader : GenericDictionaryUploader<V006Dictionary>
    {
        private readonly IV006Repository _repository;
        public V006Uploader(
            IDictPropertyRepository dictPropertyRepository,
            IV006Repository repository) : base(dictPropertyRepository)
        {
            _repository = repository;
        }

        public bool UploadFromJson(DictionaryDataDTO dictionaryData, DictVersion version)
        {
            List<V006Dictionary> newData = ConvertJsonToModel(dictionaryData, version);
            List<V006Dictionary> uploadedData = new List<V006Dictionary>();

            foreach (V006Dictionary entry in newData)
            {
                var existing = _repository.FindBy(x => x.Code == entry.Code && !x.IsDeleted);
                if (!existing.Any())
                {
                    uploadedData.Add(entry);
                    continue;
                }
                bool added = true;

                foreach (var item in existing)
                {
                    if (item.Name == entry.Name)
                    {
                        if(item.BeginDate == entry.BeginDate)
                        {
                            if (entry.EndDate != null)
                            {
                                _repository.VirtualDelete(item, 0); // TODO: Change this when you'll implement users
                            }
                            else added = false;
                            continue;
                        }
                        else _repository.VirtualDelete(item, 0); // TODO: Change this when you'll implement users
                    }
                }
                if (added) uploadedData.Add(entry);
            }
            if(!uploadedData.Any()) return false;

            foreach (var item in uploadedData)
            {
                item.DictVersionId = version.Id;
                _repository.Add(item);
            }

            return true;
        }

        public override List<V006Dictionary> ConvertJsonToModel(DictionaryDataDTO dictionaryData, DictVersion version)
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
