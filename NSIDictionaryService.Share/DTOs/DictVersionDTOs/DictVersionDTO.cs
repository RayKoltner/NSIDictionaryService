namespace NSIDictionaryService.Share.DTOs
{
    public class DictVersionDTO
    {
        public int DictionaryCodeId { get; set; }

        public decimal VersionCode { get; set; }

        public string PublicationDate { get; set; } = string.Empty;
    }
}
