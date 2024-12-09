namespace NSIDictionaryService.Share.DTOs
{
    public class DictVersionDTO
    {
        public string DictionaryCode { get; set; } = string.Empty;

        public decimal VersionCode { get; set; }

        public string PublicationDate { get; set; } = string.Empty;
    }
}
