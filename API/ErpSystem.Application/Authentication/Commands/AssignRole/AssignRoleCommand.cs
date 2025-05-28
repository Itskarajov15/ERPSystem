using MediatR;

namespace ErpSystem.Application.Authentication.Commands.AssignRole;

public record AssignRoleCommand(Guid UserId, string RoleName) : IRequest;
