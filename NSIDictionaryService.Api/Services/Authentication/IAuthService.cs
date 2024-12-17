using NSIDictionaryService.Data.Models.Users;

namespace NSIDictionaryService.Api.Services.Authentication
{
    public interface IAuthService
    {
        Task<(int, string)> Registration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
    }
}
