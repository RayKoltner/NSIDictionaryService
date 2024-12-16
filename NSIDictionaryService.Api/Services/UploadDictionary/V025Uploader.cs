using NSIDictionaryService.Api.Repositories.Dictionaries;
using NSIDictionaryService.Api.Repositories.Upload;
using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Services.UploadDictionary
{
    public class V025Uploader: GenericDictionaryUploader<V025Dictionary, decimal>
    {
        private readonly IV025Repository _repository;
        private readonly ILogger<V025Uploader> _logger;
        private readonly IChangeRepository _changeRepository;
        public V025Uploader(
            IDictPropertyRepository dictPropertyRepository,
            IV025Repository repository,
            ILogger<V025Uploader> logger,
            IChangeRepository changeRepository) : base(dictPropertyRepository)
        {
            _repository = repository;
            _logger = logger;
            _changeRepository = changeRepository;
        }

        public async Task<bool> UploadFromJson(DictionaryDataDTO dictionaryData, DictVersion version, int uploadFileId)
        {
            List<V025Dictionary> newData = ConvertJsonToModel(dictionaryData, version);
            List<V025Dictionary> uploadedData = new List<V025Dictionary>();

            foreach (V025Dictionary entry in newData)
            {
                var existing = _repository.FindBy(
                    x => x.Code == entry.Code && !x.IsDeleted && x.BeginDate == entry.BeginDate);
                if (!existing.Any())
                {
                    uploadedData.Add(entry);
                    string logEntry = $"В словаре V025 добавлена строка {entry.ToString()}";
                    _logger.LogInformation(logEntry);
                    _changeRepository.Add(new Change(uploadFileId, logEntry));
                    continue;
                }
                if (existing.Count() > 1) throw new Exception("Найдено несколько записей с одинаковыми ключом и датой начала");

                V025Dictionary existingEntry = existing.First();
                if (entry.EndDate != null)
                {
                    if (existingEntry.EndDate == entry.EndDate) continue;
                    uploadedData.Add(entry);
                    await _repository.VirtualDelete(existingEntry.Id, 0); //TODO : Add users
                    await _repository.SaveChangesAsync();

                    string logEntry = $"В словаре V025 заменена строка {existingEntry.ToString()} на {entry.ToString()}";
                    _logger.LogInformation(logEntry);
                    _changeRepository.Add(new Change(uploadFileId, logEntry));
                }

            }
            if (!uploadedData.Any()) return false;

            foreach (var item in uploadedData)
            {
                item.DictVersionId = version.Id;
                _repository.Add(item);
            }
            await _repository.SaveChangesAsync();

            return true;
        }

        public DictionaryDataDTO replaceDot(DictionaryDataDTO dto)
        {
            foreach (var item in dto.ResponseData)
            {
                foreach(var pair in item)
                {
                    if (pair.Column == "IDPC")
                    { 
                        pair.Value = pair.Value.Replace('.', ',');
                    }
                }
            }
            return dto;
        }

        public override List<V025Dictionary> ConvertJsonToModel(DictionaryDataDTO dictionaryData, DictVersion version)
        {
            dictionaryData = replaceDot(dictionaryData); // I dont like this one bit

            List<V025Dictionary> emptyDicts = new List<V025Dictionary>();
            for (int i = 0; i < dictionaryData.ResponseData.Count; i++)
            {
                emptyDicts.Add(new V025Dictionary());
            }

            var result = base.FillWithData(dictionaryData, emptyDicts, version);
            return result;
        }
    }
}
