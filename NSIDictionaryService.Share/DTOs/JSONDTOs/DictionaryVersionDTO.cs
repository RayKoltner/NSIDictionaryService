using System.Text.Json.Serialization;

namespace NSIDictionaryService.Share.DTOs
{
    public class DictionaryVersionDTO
    {
        [JsonPropertyName("list")]
        public List<DictionaryVersionDataDTO> VersionData { get; set; }
    }
}
