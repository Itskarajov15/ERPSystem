using MediatR;

namespace ErpSystem.Application.Authentication.Commands.CreateRole;

public record CreateRoleCommand(string Name, string Description, List<Guid> PermissionIds)
    : IRequest<Guid>;
