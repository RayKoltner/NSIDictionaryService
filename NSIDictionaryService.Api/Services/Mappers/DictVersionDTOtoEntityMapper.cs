using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Services.Mappers
{
    public static class DictVersionDTOtoEntityMapper
    {
        public static DictVersion Convert(DictVersionDTO dto)
        {
            var converted = new DictVersion();
            converted.DictionaryCode = dto.DictionaryCode;
            converted.VersionCode = dto.VersionCode;
            converted.PublicationDate = DateTime.Parse(dto.PublicationDate);

            return converted;
        }
    }
}
