namespace NSIDictionaryService.Share.DTOs
{
    public class DictVersionPutDTO
    {
        public int Id { get; set; }

        public decimal VersionCode { get; set; }

        public string PublicationDate { get; set; } = string.Empty;
    }
}
