using ErpSystem.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ErpSystem.Application.Authentication.Commands.EditRole;

internal class EditRoleCommandHandler : IRequestHandler<EditRoleCommand>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public Task Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
