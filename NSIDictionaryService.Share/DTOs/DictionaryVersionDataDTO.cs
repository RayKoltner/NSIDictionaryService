using System.Text.Json.Serialization;

namespace NSIDictionaryService.Share.DTOs
{
    public class DictionaryVersionDataDTO
    {
        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        [JsonPropertyName("createDate")]
        public DateTime CreateDate { get; set; }

        [JsonPropertyName("publishDate")]
        public DateTime UpdateDate { get; set; }
    }
}
