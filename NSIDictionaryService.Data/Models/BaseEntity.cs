
using System.ComponentModel.DataAnnotations;

namespace NSIDictionaryService.Data.Models
{
    public class BaseEntity
    {
        [Display(Name = "Id в базе")]
        public int Id;

        [Display(Name = "Признак удаления")]
        public bool IsDeleted;

        [Display(Name = "Пользователь, удаливший запись")]
        public int deletedUserId;
    }
}
