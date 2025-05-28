using ErpSystem.Frontend.Core.Models.Authentication;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginViewModel model);

    Task<bool> RegisterAsync(RegisterViewModel model);

    Task LogoutAsync();
}
