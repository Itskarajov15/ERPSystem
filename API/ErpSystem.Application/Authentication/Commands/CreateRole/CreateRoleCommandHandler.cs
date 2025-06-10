using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Interfaces;
using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Authentication.Commands.CreateRole;

internal class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
{
    private readonly IIdentityService _identityService;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IRepository _repository;

    public CreateRoleCommandHandler(
        IIdentityService identityService,
        RoleManager<ApplicationRole> roleManager,
        IRepository repository
    )
    {
        _identityService = identityService;
        _roleManager = roleManager;
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _identityService.CreateRoleAsync(request.Name, request.Description);

        if (role == null)
        {
            throw new Exception(RoleErrorKeys.RoleCreationFailed);
        }

        if (request.PermissionIds.Any())
        {
            var roleEntity = await _roleManager.FindByIdAsync(role.Id);
            if (roleEntity != null)
            {
                var permissions = await _repository
                    .AllReadOnly<RoutePermission>()
                    .Where(rp => request.PermissionIds.Contains(rp.Id))
                    .ToListAsync(cancellationToken);

                await _repository.AddRangeAsync(
                    permissions.Select(p => new RoleRoutePermission
                    {
                        RoleId = roleEntity.Id,
                        RoutePermissionId = p.Id,
                    })
                );

                await _repository.SaveChangesAsync();
            }
        }

        return Guid.Parse(role.Id);
    }
}
