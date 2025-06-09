using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Authentication.Commands.EditRole;

internal class EditRoleCommandHandler : IRequestHandler<EditRoleCommand>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IRepository _repository;

    public EditRoleCommandHandler(RoleManager<ApplicationRole> roleManager, IRepository repository)
    {
        _roleManager = roleManager;
        _repository = repository;
    }

    public async Task Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

        if (role == null)
        {
            throw new NotFoundException(RoleErrorKeys.RoleNotFound);
        }

        if (
            request.Dto.Name != role.Name
            && await _roleManager.FindByNameAsync(request.Dto.Name) != null
        )
        {
            throw new ArgumentException(RoleErrorKeys.RoleAlreadyExists);
        }

        role.Name = request.Dto.Name;
        role.NormalizedName = request.Dto.Name.ToUpper();
        role.Description = request.Dto.Description;

        var permissions = await _repository
            .AllReadOnly<RoutePermission>()
            .Where(rp => request.Dto.PermissionIds.Contains(rp.Id))
            .ToListAsync();

        var oldRolePermissions = await _repository
            .AllReadOnly<RoleRoutePermission>()
            .Where(rrp => rrp.RoleId == role.Id)
            .ToListAsync();

        _repository.DeleteRange(oldRolePermissions);

        await _repository.AddRangeAsync(
            permissions.Select(p => new RoleRoutePermission
            {
                RoleId = role.Id,
                RoutePermissionId = p.Id,
            })
        );

        await _repository.SaveChangesAsync();
    }
}
