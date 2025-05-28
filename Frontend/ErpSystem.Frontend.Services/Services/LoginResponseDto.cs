namespace ErpSystem.Frontend.Core.Services;

public partial class AuthService
{
    private class LoginResponseDto
    {
        public string UserId { get; set; } = string.Empty;

        public string? UserName { get; set; }

        public string AccessToken { get; set; } = string.Empty;
    }
}
