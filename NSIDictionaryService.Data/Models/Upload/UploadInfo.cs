namespace NSIDictionaryService.Data.Models
{
    public class UploadInfo: BaseEntity
    {
        //Загрузки
        public int UploadingUserId { get; set; }

        public DateTime UploadDate { get; set; }

        public string DictCode { get; set; }

        public int DictVersionId { get; set; }
        public virtual DictVersion? DictVersion { get; private set; }

        public int UploadMethodId { get; set; }
        public virtual UploadMethod? UploadMethod { get; set; }

        public int UploadResultId { get; set; }
        public virtual UploadResult? UploadResult { get; set; }

        public string ErrorDescription { get; set; } = String.Empty;

        public UploadInfo() { }

        public UploadInfo(
            int uploadingUserId, 
            DateTime uploadDate, 
            string dictCode,
            int dictVersionId, 
            int uploadMethodId, 
            int uploadResultId)
        {
            UploadingUserId = uploadingUserId;
            UploadDate = uploadDate;
            DictCode = dictCode;
            DictVersionId = dictVersionId;
            UploadMethodId = uploadMethodId;
            UploadResultId = uploadResultId;
        }
    }
}
