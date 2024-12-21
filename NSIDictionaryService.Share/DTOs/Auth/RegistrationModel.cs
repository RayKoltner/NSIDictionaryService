using System.ComponentModel.DataAnnotations;

namespace NSIDictionaryService.Share.DTOs.Auth
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Логин обязателен")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        public string? Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email обязателен")]
        public string? Email { get; set; }

        [MinLength(8, ErrorMessage = "Пароль должен быть не менее 8 символов")]
        [Required(ErrorMessage = "Пароль обязателен")]
        public string? Password { get; set; }
    }
}
