using ErpSystem.Application.Common.Interfaces;
using MediatR;

namespace ErpSystem.Application.Authentication.Commands.CreateRole;

internal class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, string>
{
    private readonly IIdentityService _identityService;

    public CreateRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _identityService.CreateRoleAsync(request.Name, request.Description);

        if (role == null)
        {
            throw new Exception("Role creation failed");
        }

        return role.Id;
    }
}
