using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ErpSystem.Frontend.Core.Services;

public partial class AuthService : IAuthService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IApiService apiService,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AuthService> logger
    )
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<bool> LoginAsync(LoginViewModel model)
    {
        try
        {
            var response = await _apiService.PostAsync<LoginResponseDto>(
                "/api/authentication/login",
                new { UserName = model.Username, Password = model.Password }
            );

            if (response?.AccessToken == null)
                return false;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(response.AccessToken);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, response.UserId),
                new(ClaimTypes.Name, response.UserName ?? model.Username),
                new("jwt_token", response.AccessToken),
            };

            var roleClaims = jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            claims.AddRange(roleClaims);

            foreach (var roleClaim in roleClaims)
            {
                _logger.LogInformation("Adding role claim: {Role}", roleClaim.Value);
            }

            var permissionClaims = jwtToken.Claims.Where(c => c.Type == "Permission").ToList();
            claims.AddRange(permissionClaims);

            foreach (var permissionClaim in permissionClaims)
            {
                _logger.LogInformation(
                    "Adding permission claim: {Permission}",
                    permissionClaim.Value
                );
            }

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            _logger.LogInformation(
                "Created ClaimsIdentity with {ClaimCount} claims",
                claimsIdentity.Claims.Count()
            );

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12),
            };

            await _httpContextAccessor.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login process");
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        await _httpContextAccessor.HttpContext!.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );
    }

    public async Task<bool> RegisterAsync(RegisterViewModel model)
    {
        try
        {
            await _apiService.PostAsync<string>(
                "/api/authentication/register",
                new
                {
                    UserName = model.Email,
                    model.Password,
                    model.Email,
                    model.FirstName,
                    model.LastName,
                }
            );

            return true;
        }
        catch
        {
            return false;
        }
    }
}
