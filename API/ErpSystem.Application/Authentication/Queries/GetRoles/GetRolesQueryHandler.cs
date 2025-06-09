using ErpSystem.Application.Authentication.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Authentication.Queries.GetRoles;

internal class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PageResult<RoleDto>>
{
    private readonly IRepository _repository;

    public GetRolesQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<RoleDto>> Handle(
        GetRolesQuery request,
        CancellationToken cancellationToken
    )
    {
        var filterBy = ComposeFilterBy(request.RoleFilters);

        var roles = await _repository.GetPaginatedAsync<ApplicationRole, RoleDto>(
            request.PaginationParams,
            query =>
                query
                    .Include(r => r.RoleRoutePermissions)
                    .ThenInclude(rrp => rrp.RoutePermission)
                    .Select(r => new RoleDto
                    {
                        Id = r.Id.ToString(),
                        Name = r.Name!,
                        Description = r.Description,
                        Permissions = r
                            .RoleRoutePermissions.Select(rrp => new PermissionDto
                            {
                                Id = rrp.RoutePermission.Id,
                                Name =
                                    $"{rrp.RoutePermission.ControllerName}.{rrp.RoutePermission.ActionName}",
                                ControllerName = rrp.RoutePermission.ControllerName,
                                ActionName = rrp.RoutePermission.ActionName,
                                Endpoint =
                                    $"{rrp.RoutePermission.ControllerName}/{rrp.RoutePermission.ActionName}",
                            })
                            .ToList(),
                    }),
            filterBy
        );

        return roles;
    }

    private Func<IQueryable<ApplicationRole>, IQueryable<ApplicationRole>> ComposeFilterBy(
        RoleFilters? roleFilters
    ) =>
        query =>
        {
            if (roleFilters == null)
            {
                return query;
            }

            if (roleFilters.SearchTerm != null)
            {
                var searchTermLowerCase = roleFilters.SearchTerm.ToLower();

                query = query.Where(r =>
                    r.Name!.ToLower().Contains(searchTermLowerCase)
                    || (
                        r.Description != null
                        && r.Description.ToLower().Contains(searchTermLowerCase)
                    )
                );
            }

            return query;
        };
}
