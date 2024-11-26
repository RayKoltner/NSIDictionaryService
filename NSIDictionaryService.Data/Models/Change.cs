namespace NSIDictionaryService.Data.Models
{
    public class Change: BaseEntity
    {
        //Класс записей логов изменений
        public int UploadId { get; set; }
        public string Comments { get; set; } = string.Empty;
    }
}
