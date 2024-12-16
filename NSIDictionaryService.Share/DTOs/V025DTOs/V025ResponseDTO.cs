using NSIDictionaryService.Data.Models.Dictionaries;

namespace NSIDictionaryService.Share.DTOs.V025DTOs
{
    public class V025ResponseDTO
    {
        public decimal Code { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Comments { get; set; } = string.Empty;

        public int DictVersionId { get; set; }
        public DateTime CreateDate { get; set; }
        public int EditUserId { get; set; }
        public DateTime EditDate { get; set; }
        public int DeletedUserId { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
        public V025ResponseDTO(V025Dictionary dictionary)
        {
            this.Code = dictionary.Code;
            this.BeginDate = dictionary.BeginDate;
            this.EndDate = dictionary.EndDate;
            this.Name = dictionary.Name;
            this.Comments = dictionary.Comments;

            this.CreateDate = dictionary.CreateDate;
            this.EditUserId = dictionary.EditUserId;
            this.EditDate = dictionary.EditDate;
            this.DeletedUserId = dictionary.DeletedUserId;
            this.DeletedDate = dictionary.DeletedDate;
            this.IsDeleted = dictionary.IsDeleted;
        }
    }
}
