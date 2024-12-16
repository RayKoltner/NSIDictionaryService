using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Services.Mappers
{
    public static class DictVersionDTOtoEntityMapper
    {
        public static DictVersion Convert(DictVersionDTO dto)
        {
            var converted = new DictVersion();
            converted.DictCodeId = dto.DictionaryCodeId;
            converted.VersionCode = dto.VersionCode;
            converted.PublicationDate = DateTime.ParseExact(dto.PublicationDate, "dd.MM.yyyy", null);

            return converted;
        }
    }
}
