using System.ComponentModel.DataAnnotations;

namespace NSIDictionaryService.Data.Models.Dictionaries
{
    internal class V012Dictionary:BaseDictionaryType
    {
        [Display(Name = "Код условия оказания медпомощи")]
        public int UMPId { get; set; }
    }
}
