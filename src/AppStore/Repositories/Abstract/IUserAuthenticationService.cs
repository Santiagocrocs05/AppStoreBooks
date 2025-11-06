using AppStore.Models.DTOs;

namespace AppStore.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel login);
        Task<bool> LogoutAsync();
    }
}