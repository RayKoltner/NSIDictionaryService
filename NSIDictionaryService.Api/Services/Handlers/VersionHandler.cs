using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Share.DTOs;
using System.Globalization;
using NSIDictionaryService.Api.Exceptions;

namespace NSIDictionaryService.Api.Services.Handlers
{
    public class VersionHandler
    {
        private readonly IDictVersionRepository _repository;

        public VersionHandler(IDictVersionRepository repository)
        {
            _repository = repository;
        }

        public async Task<DictVersion> HandleVersion(DictionaryVersionDataDTO versionDataDTO, int dictIdentifierId)
        {
            DictVersion version;
            decimal versionCode = decimal.Parse(versionDataDTO.Version, CultureInfo.InvariantCulture);

            var existingVersion = await _repository.FirstAsync(
                x => x.DictCodeId == dictIdentifierId && !x.IsDeleted);

            if (existingVersion != null && 
                versionCode <= existingVersion.VersionCode)
            {
                version = existingVersion;
                throw new InvalidVersionException("Такая или большая версия уже существует", version);
            }
            if (existingVersion != null) await _repository.VirtualDelete(existingVersion, 0); // TODO : Add users

            version = new DictVersion(dictIdentifierId,
                versionCode,
                versionDataDTO.CreateDate.ToDateTime(TimeOnly.MinValue));

            _repository.Add(version);
            await _repository.SaveChangesAsync();
            return version;
        }
    }
}
