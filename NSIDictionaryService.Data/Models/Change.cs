namespace NSIDictionaryService.Data.Models
{
    public class Change: BaseEntity
    {
        //Класс записей логов изменений
        public int UploadInfoId { get; set; }
        public UploadInfo? UploadInfo { get; private set; }
        public string Comments { get; set; } = string.Empty;

        public Change() { }

        public Change(int uploadInfoId, string comments)
        {
            UploadInfoId = uploadInfoId;
            Comments = comments;
        }
    }
}
