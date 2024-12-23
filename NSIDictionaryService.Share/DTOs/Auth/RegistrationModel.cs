using NSIDictionaryService.Share.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace NSIDictionaryService.Share.DTOs.Auth
{
    public class RegistrationModel
    {
        [LatinLettersOrDigits]
        [Required(ErrorMessage = "Логин обязателен")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Указанные данные не соответствуют формату электронной почты")]
        [Required(ErrorMessage = "Email обязателен")]
        public string? Email { get; set; }

        [MinLength(8, ErrorMessage = "Пароль должен быть не менее 8 символов")]
        [Required(ErrorMessage = "Пароль обязателен")]
        [LatinLettersOrDigits]
        public string? Password { get; set; }
    }
}
