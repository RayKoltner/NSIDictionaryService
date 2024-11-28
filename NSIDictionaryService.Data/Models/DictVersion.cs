using System.ComponentModel.DataAnnotations;

namespace NSIDictionaryService.Data.Models
{
    public class DictVersion: BaseEntity
    {
        [Display(Name = "Код словаря")]
        public string DictionaryCode { get; set; } = string.Empty;

        [Display(Name = "Номер версии")]
        public string VersionCode { get; set; } = string.Empty;

        [Display(Name = "Дата публикации версии")]
        public DateTime PublicationDate { get; set; }
    }
}
