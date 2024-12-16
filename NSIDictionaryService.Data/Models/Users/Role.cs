namespace NSIDictionaryService.Data.Models
{
    public class Role: BaseEntity
    {
        //Роли пользователей
        public string RoleName { get; set; } = string.Empty;

        public string Privileges { get; set; } = string.Empty;
    }
}
