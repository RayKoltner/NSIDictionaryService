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

        public async Task<bool> UploadFromJson(DictionaryDataDTO dictionaryData, DictVersion version)
        {
            List<V006Dictionary> newData = ConvertJsonToModel(dictionaryData, version);
            List<V006Dictionary> uploadedData = new List<V006Dictionary>();

            foreach (V006Dictionary entry in newData)
            {
                var existing = _repository.FindBy(
                    x => x.Code == entry.Code && !x.IsDeleted && x.BeginDate == entry.BeginDate);
                if (!existing.Any())
                {
                    uploadedData.Add(entry);
                    continue;
                }
                if (existing.Count() > 1) throw new Exception("Найдено несколько записей с одинаковыми ключом и датой начала");

                V006Dictionary existingEntry = existing.First();
                if (entry.EndDate != null)
                {
                    if (existingEntry.EndDate == entry.EndDate) continue;
                    uploadedData.Add(entry);
                    await _repository.VirtualDelete(existingEntry.Id, 0); //TODO : Add users
                    await _repository.SaveChangesAsync();
                }
                
            }
            if(!uploadedData.Any()) return false;

            foreach (var item in uploadedData)
            {
                item.DictVersionId = version.Id;
                _repository.Add(item);
            }
            await _repository.SaveChangesAsync();

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
