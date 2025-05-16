using ErpSystem.Application.Common.Interfaces;
using MediatR;

namespace ErpSystem.Application.Authentication.Commands.AssignRole;

internal class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand>
{
    private readonly IIdentityService _identityService;

    public AssignRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        await _identityService.AddToRoleAsync(request.UserId, request.RoleName);
    }
}
