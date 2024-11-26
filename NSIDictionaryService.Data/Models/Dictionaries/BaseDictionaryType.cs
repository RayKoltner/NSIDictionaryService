using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSIDictionaryService.Data.Models.Dictionaries
{
    public class BaseDictionaryType: BaseEntity
    {
        [Display(Name = "Код")]
        public int Code { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Начало")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Окончание")]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        [MaxLength(350)]
        [Display(Name = "Наименование")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(350)]
        public string Comments { get; set; } = string.Empty;

        public BaseDictionaryType()
        {
        }
    }
}
