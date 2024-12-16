using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Api.Services.Mappers
{
    public static class V006DTOtoEntityMapper
    {
        public static V006Dictionary Convert(V006DTO dto)
        {
            V006Dictionary entry = new V006Dictionary();
            entry.Code = dto.Code;
            entry.Name = dto.Name;
            entry.BeginDate = dto.BeginDate ?? null;
            entry.EndDate = dto.EndDate ?? null;
            entry.Comments = dto.Comments;
            entry.DictVersionId = dto.DictVersionId;

            return entry;
        }
    }
}
