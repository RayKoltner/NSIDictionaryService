namespace NSIDictionaryService.Share.DTOs
{
    public class UploadInfoResponseDTO
    {
        public int Id { get; set; }
        public int UploadingUserId { get; set; }

        public DateTime UploadDate { get; set; }

        public string DictCode { get; set; } = string.Empty;

        public int DictVersionId { get; set; }

        public int UploadMethodId { get; set; }

        public int UploadResultId { get; set; }

        public string ErrorDescription { get; set; } = String.Empty;

    }
}
