using NSIDictionaryService.Api.Repositories.Dictionaries;
using NSIDictionaryService.Api.Repositories.Upload;
using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Services.UploadDictionary
{
    public class V012Uploader : GenericDictionaryUploader<V012Dictionary, int>
    {
        private readonly IV012Repository _repository;
        private readonly ILogger<V012Uploader> _logger;
        private readonly IChangeRepository _changeRepository;
        public V012Uploader(
            IDictPropertyRepository dictPropertyRepository,
            IV012Repository repository,
            ILogger<V012Uploader> logger,
            IChangeRepository changeRepository) : base(dictPropertyRepository)
        {
            _repository = repository;
            _logger = logger;
            _changeRepository = changeRepository;
        }

        public async Task<bool> UploadFromJson(DictionaryDataDTO dictionaryData, DictVersion version, int uploadFileId)
        {
            List<V012Dictionary> newData = ConvertJsonToModel(dictionaryData, version);
            List<V012Dictionary> uploadedData = new List<V012Dictionary>();

            foreach (V012Dictionary entry in newData)
            {
                var existing = _repository.FindBy(
                    x => x.Code == entry.Code && !x.IsDeleted && x.BeginDate == entry.BeginDate && x.UMPId == entry.UMPId);
                if (!existing.Any())
                {
                    uploadedData.Add(entry);
                    string logEntry = $"В словаре V012 добавлена строка {entry.ToString()}";
                    _logger.LogInformation(logEntry);
                    _changeRepository.Add(new Change(uploadFileId, logEntry));
                    continue;
                }
                if (existing.Count() > 1) throw new Exception("Найдено несколько записей с одинаковыми ключом и датой начала");

                V012Dictionary existingEntry = existing.First();
                if (entry.EndDate != null)
                {
                    if (existingEntry.EndDate == entry.EndDate) continue;
                    uploadedData.Add(entry);
                    await _repository.VirtualDelete(existingEntry.Id, 0); //TODO : Add users
                    await _repository.SaveChangesAsync();

                    string logEntry = $"В словаре V012 заменена строка {existingEntry.ToString()} на {entry.ToString()}";
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

        public override List<V012Dictionary> ConvertJsonToModel(DictionaryDataDTO dictionaryData, DictVersion version)
        {
            List<V012Dictionary> emptyDicts = new List<V012Dictionary>();
            for (int i = 0; i < dictionaryData.ResponseData.Count; i++)
            {
                emptyDicts.Add(new V012Dictionary());
            }

            var result = base.FillWithData(dictionaryData, emptyDicts, version);
            return result;
        }
    }
}
