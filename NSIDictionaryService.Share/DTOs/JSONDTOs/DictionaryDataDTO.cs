using System.Text.Json.Serialization;

namespace NSIDictionaryService.Share.DTOs
{
    public class DictionaryDataDTO
    {
        [JsonPropertyName("list")]
        public List<List<JsonPair>> ResponseData { get; set; } = null!;
    }
}
