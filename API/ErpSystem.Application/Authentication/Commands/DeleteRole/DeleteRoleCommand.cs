using MediatR;

namespace ErpSystem.Application.Authentication.Commands.DeleteRole;

public record DeleteRoleCommand(Guid Id) : IRequest;
