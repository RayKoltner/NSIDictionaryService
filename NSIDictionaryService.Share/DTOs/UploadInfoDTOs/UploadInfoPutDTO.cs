namespace NSIDictionaryService.Share.DTOs
{
    public class UploadInfoPutDTO
    {
        public int Id { get; set; }
        public string DictCode { get; set; } = string.Empty;

        public int DictVersionId { get; set; }
    }
}
