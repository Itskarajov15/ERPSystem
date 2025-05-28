using MediatR;

namespace ErpSystem.Application.Authentication.Commands.RemoveRole;

public record RemoveRoleCommand(Guid UserId, string RoleName) : IRequest;
