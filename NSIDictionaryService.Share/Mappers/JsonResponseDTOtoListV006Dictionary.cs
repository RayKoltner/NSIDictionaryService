using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs;

namespace NSIDictionaryService.Share.Mappers
{
    public static class JsonResponseDTOtoListV006Dictionary
    {
        static List<V006Dictionary> Convert(DictionaryDataDTO convertable)
        {
            List<V006Dictionary> converted = new List<V006Dictionary>();
            foreach (var entry in convertable.ResponseData)
            {
                V006Dictionary dictionary = new V006Dictionary();
                dictionary.Code = int.Parse(entry.Where(x => x.Column == "IDUMP").FirstOrDefault().Value);
                dictionary.Name = entry.Where(x => x.Column == "UMPNAME").FirstOrDefault().Value;
                dictionary.BeginDate = DateTime.Parse(entry.Where(x => x.Column == "DATEBEG").FirstOrDefault().Value);
                dictionary.EndDate = DateTime.Parse(entry.Where(x => x.Column == "DATEEND").FirstOrDefault().Value ?? "01.01.0001");
                converted.Add(dictionary);
            }
            return converted;
        }
    }
}
