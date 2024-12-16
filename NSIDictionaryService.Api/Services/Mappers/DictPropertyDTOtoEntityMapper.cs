using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Services.Mappers
{
    public static class DictPropertyDTOtoEntityMapper
    {
        public static DictProperty Convert(DictPropertyDTO dto)
        {
            DictProperty dictProperty = new DictProperty()
            {
                DictCodeId = dto.DictionaryCodeId,
                PropertyCode = dto.PropertyCode,
                PropertyName = dto.PropertyName
            };
            return dictProperty;
        }
    }
}
