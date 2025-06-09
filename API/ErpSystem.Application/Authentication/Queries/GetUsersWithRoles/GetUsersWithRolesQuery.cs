using ErpSystem.Application.Authentication.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;

namespace ErpSystem.Application.Authentication.Queries.GetUsersWithRoles;

public record GetUsersWithRolesQuery(PaginationParams PaginationParams, UserFilters? UserFilters)
    : IRequest<PageResult<UserWithRolesDto>>;
