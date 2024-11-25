using System.ComponentModel.DataAnnotations;

namespace NSIDictionaryService.Data.Models.Dictionaries
{
    public class BaseDictionaryType: BaseEntity
    {
        //Базовый класс словаря, хранит общие поля записей
        [Display(Name = "Код")]
        public int Code { get; set; }

        [Display(Name = "Начало")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Окончание")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; } = string.Empty;

        public BaseDictionaryType()
        {
            BeginDate = DateTime.MinValue;
            EndDate = DateTime.MaxValue;
        }
    }
}
