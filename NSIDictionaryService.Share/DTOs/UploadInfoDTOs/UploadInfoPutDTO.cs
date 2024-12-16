namespace NSIDictionaryService.Share.DTOs
{
    public class UploadInfoPutDTO
    {
        public int Id { get; set; }
        public string DictCode { get; set; } = String.Empty;

        public int DictVersionId { get; set; }
    }
}
