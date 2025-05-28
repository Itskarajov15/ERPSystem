using ErpSystem.Application.Authentication.DTOs;
using MediatR;

namespace ErpSystem.Application.Authentication.Commands.EditRole;

public record EditRoleCommand(Guid RoleId, EditRoleDto Dto) : IRequest;
