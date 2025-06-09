using ErpSystem.Application.Authentication.DTOs;
using MediatR;

namespace ErpSystem.Application.Authentication.Queries.GetRoleById;

public record GetRoleByIdQuery(Guid RoleId) : IRequest<RoleDto?>;
