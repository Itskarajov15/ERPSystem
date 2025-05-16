using ErpSystem.Application.Common.Interfaces;
using MediatR;

namespace ErpSystem.Application.Authentication.Commands.RemoveRole;

internal class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand>
{
    private readonly IIdentityService _identityService;

    public RemoveRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
    {
        await _identityService.RemoveRoleAsync(request.UserId, request.RoleName);
    }
}
