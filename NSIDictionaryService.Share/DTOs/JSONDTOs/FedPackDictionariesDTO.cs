using System.Text.Json.Serialization;

namespace NSIDictionaryService.Share.DTOs
{
    public class FedPackDictionariesDTO
    {
        [JsonPropertyName("dictionaryVersions")]
        public List<FedPackDictionariesDataDTO> VersionData { get; set; }
    }
}
