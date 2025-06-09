using ErpSystem.Application.Authentication.DTOs;
using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Authentication.Queries.GetRoleById;

internal class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IRepository _repository;

    public GetRoleByIdQueryHandler(RoleManager<ApplicationRole> roleManager, IRepository repository)
    {
        _roleManager = roleManager;
        _repository = repository;
    }

    public async Task<RoleDto?> Handle(
        GetRoleByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

        if (role == null)
        {
            throw new NotFoundException(RoleErrorKeys.RoleNotFound);
        }

        var permissions = await _repository
            .AllReadOnly<RoleRoutePermission>()
            .Include(rrp => rrp.RoutePermission)
            .Where(rrp => rrp.RoleId == role.Id)
            .Select(rrp => new PermissionDto
            {
                Id = rrp.RoutePermission.Id,
                Name = $"{rrp.RoutePermission.ControllerName}.{rrp.RoutePermission.ActionName}",
                ControllerName = rrp.RoutePermission.ControllerName,
                ActionName = rrp.RoutePermission.ActionName,
                Endpoint = $"{rrp.RoutePermission.ControllerName}/{rrp.RoutePermission.ActionName}",
            })
            .ToListAsync(cancellationToken);

        return new RoleDto
        {
            Id = role.Id.ToString(),
            Name = role.Name!,
            Description = role.Description,
            Permissions = permissions,
        };
    }
}
