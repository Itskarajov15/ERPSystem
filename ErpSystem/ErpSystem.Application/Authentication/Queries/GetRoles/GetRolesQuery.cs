using ErpSystem.Application.Authentication.DTOs;
using ErpSystem.Domain.Common.Pagination;
using MediatR;

namespace ErpSystem.Application.Authentication.Queries.GetRoles;

public record GetRolesQuery(PaginationParams PaginationParams) : IRequest<PageResult<RoleDto>>;
