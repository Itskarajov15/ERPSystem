namespace ErpSystem.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<string> CreateUserAsync(string userName, string password);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task AddToRoleAsync(string userId, string role);

    Task DeleteUserAsync(string userId);

    Task<string> AuthenticateAsync(string userName, string password);
}
