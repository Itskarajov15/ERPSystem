using ErpSystem.Application.Common.Interfaces;
using MediatR;

namespace ErpSystem.Application.Authentication.Commands.DeleteRole;

internal class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IIdentityService _identityService;

    public DeleteRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        await _identityService.DeleteRoleAsync(request.Id);
    }
}
