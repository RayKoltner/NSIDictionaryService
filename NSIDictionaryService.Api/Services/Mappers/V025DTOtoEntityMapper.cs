using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs.V025DTOs;

namespace NSIDictionaryService.Api.Services.Mappers
{
    public static class V025DTOtoEntityMapper
    {
        public static V025Dictionary Convert(V025DTO dto)
        {
            V025Dictionary entry = new V025Dictionary();
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
