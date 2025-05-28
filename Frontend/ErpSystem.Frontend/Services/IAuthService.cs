using ErpSystem.Frontend.Web.Models.Authentication;

namespace ErpSystem.Frontend.Web.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginViewModel model);
    Task<bool> RegisterAsync(RegisterViewModel model);
    Task LogoutAsync();
} 