using System.Text.Json.Serialization;

namespace NSIDictionaryService.Share.DTOs
{
    public class JsonPair
    {
        [JsonPropertyName("column")]
        public string Column { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;
    }
}
