using System.ComponentModel.DataAnnotations;

namespace NSIDictionaryService.Data.Models
{
    public class DictVersion: BaseEntity
    {
        [Display(Name = "Код словаря")]
        public string DictionaryCode { get; set; } = string.Empty;

        [Display(Name = "Номер версии")]
        public decimal VersionCode { get; set; }

        [Display(Name = "Дата публикации версии")]
        public DateTime PublicationDate { get; set; }

        public DictVersion() { }
        public DictVersion(string dictionaryCode, decimal versionCode, DateTime publicationDate)
        {
            DictionaryCode = dictionaryCode;
            VersionCode = versionCode;
            PublicationDate = publicationDate;
        }
    }
}
