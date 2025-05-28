using ErpSystem.Application.Authentication.DTOs;
using ErpSystem.Application.Common.Interfaces;
using MediatR;

namespace ErpSystem.Application.Authentication.Commands.Login;

internal class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<LoginResponseDto> Handle(
        LoginCommand request,
        CancellationToken cancellationToken
    )
    {
        var authResult = await _identityService.AuthenticateAsync(
            request.UserName,
            request.Password
        );

        if (authResult == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        return new LoginResponseDto()
        {
            UserId = authResult.UserId,
            UserName = authResult.UserName,
            AccessToken = authResult.AccessToken,
        };
    }
}
