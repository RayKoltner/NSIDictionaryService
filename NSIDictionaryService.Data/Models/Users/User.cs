using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NSIDictionaryService.Data.Models.Users;

public class User: IdentityUser<int>
{
    //Пользователи
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}
