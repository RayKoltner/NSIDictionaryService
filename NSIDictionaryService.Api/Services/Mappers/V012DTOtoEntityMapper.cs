using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs.V012DTOs;

namespace NSIDictionaryService.Api.Services.Mappers
{
    public static class V012DTOtoEntityMapper
    {
        public static V012Dictionary Convert(V012DTO dto)
        {
            V012Dictionary entry = new V012Dictionary();
            entry.Code = dto.Code;
            entry.Name = dto.Name;
            entry.BeginDate = dto.BeginDate ?? null;
            entry.EndDate = dto.EndDate ?? null;
            entry.Comments = dto.Comments;
            entry.DictVersionId = dto.DictVersionId;
            entry.UMPId = dto.UmpId;

            return entry;
        }
    }
}
