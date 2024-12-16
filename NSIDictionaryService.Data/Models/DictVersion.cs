using System.ComponentModel.DataAnnotations;

namespace NSIDictionaryService.Data.Models
{
    public class DictVersion : BaseEntity
    {
        [Display(Name = "Код словаря")]
        public int DictCodeId { get; set; }
        public virtual DictCode? DictCode { get; private set;}

        [Display(Name = "Номер версии")]
        public decimal VersionCode { get; set; }

        [Display(Name = "Дата публикации версии")]
        public DateTime PublicationDate { get; set; }

        public DictVersion() { }
        public DictVersion(int dictionaryCodeId, decimal versionCode, DateTime publicationDate)
        {
            DictCodeId = dictionaryCodeId;
            VersionCode = versionCode;
            PublicationDate = publicationDate;
        }
    }
}
