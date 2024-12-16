using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs.V021DTOs;

namespace NSIDictionaryService.Api.Services.Mappers
{
    public static class V021DTOtoEntityMapper
    {
        public static V021Dictionary Convert(V021DTO dto)
        {
            V021Dictionary entry = new V021Dictionary();
            entry.Code = dto.Code;
            entry.Name = dto.Name;
            entry.BeginDate = dto.BeginDate ?? null;
            entry.EndDate = dto.EndDate ?? null;
            entry.Comments = dto.Comments;
            entry.DictVersionId = dto.DictVersionId;

            entry.PostName = dto.PostName;
            entry.PostId = dto.PostId;

            return entry;
        }
    }
}
