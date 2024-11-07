using System.Text.Json.Serialization;

namespace NSIDictionaryService.Share.DTOs
{
    public class JsonResponseDTO
    {
        [JsonPropertyName("list")]
        public List<JsonEntryDTO> ResponseData { get; set; } = null!;
    }
}
