using System.ComponentModel.DataAnnotations;

namespace NSIDictionaryService.Data.Models.Dictionaries
{
    public class V021Dictionary:BaseDictionaryType<int>
    {
        [Display(Name = "Название должности")]
        public string PostName { get; set; } = String.Empty;

        [Display(Name = "Код должности")]
        public int PostId { get; set; }
    }
}
