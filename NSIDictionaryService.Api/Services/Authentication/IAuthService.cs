using NSIDictionaryService.Share.DTOs.Auth;

namespace NSIDictionaryService.Api.Services.Authentication
{
    public interface IAuthService
    {
        Task<(int, string)> Registration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
    }
}
