using ErpSystem.Application.Common.Interfaces;
using MediatR;

namespace ErpSystem.Application.Authentication.Commands.Register;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userId = await _identityService.CreateUserAsync(
            request.UserName,
            request.Password,
            request.Email,
            request.FirstName,
            request.LastName
        );

        if (userId == null)
        {
            throw new Exception("User registration failed");
        }
    }
}
