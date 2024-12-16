using System.ComponentModel.DataAnnotations;

namespace NSIDictionaryService.Data.Models.Dictionaries
{
    public class V012Dictionary:BaseDictionaryType<int>
    {
        [Display(Name = "Код условия оказания медпомощи")]
        public int UMPId { get; set; }
    }
}
