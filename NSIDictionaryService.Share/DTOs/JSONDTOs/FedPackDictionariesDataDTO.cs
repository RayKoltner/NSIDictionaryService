using System.Text.Json.Serialization;

namespace NSIDictionaryService.Share.DTOs
{
    public class FedPackDictionariesDataDTO
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;
    }
}
