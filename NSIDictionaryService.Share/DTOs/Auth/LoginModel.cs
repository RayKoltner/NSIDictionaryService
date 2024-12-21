using System.ComponentModel.DataAnnotations;

namespace NSIDictionaryService.Share.DTOs.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Логин обязателен")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        public string? Password { get; set; }
    }
}
