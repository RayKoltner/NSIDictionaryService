using System.Text.Json.Serialization;
using NSIDictionaryService.Share.Converters;

namespace NSIDictionaryService.Share.DTOs
{
    public class DictionaryVersionDataDTO
    {
        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        [JsonConverter(typeof(CustomDateConverter))]
        [JsonPropertyName("createDate")]
        public DateOnly CreateDate { get; set; }

        [JsonConverter(typeof(CustomDateConverter))]
        [JsonPropertyName("publishDate")]
        public DateOnly UpdateDate { get; set; }
    }
}
