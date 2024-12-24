namespace NSIDictionaryService.Share.DTOs
{
    public class DictVersionResponseDTO
    {
        public int Id { get; set; }

        public int DictionaryCodeId { get; set; }
        public string DictionaryCodeName { get; set; } = string.Empty;

        public decimal VersionCode { get; set; }

        public string PublicationDate { get; set; } = string.Empty;
    }
}
