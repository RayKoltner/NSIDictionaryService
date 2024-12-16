namespace NSIDictionaryService.Data.Models
{
    public class User: BaseEntity
    {
        //Пользователи
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public virtual Role? Role { get; private set; }
    }
}
