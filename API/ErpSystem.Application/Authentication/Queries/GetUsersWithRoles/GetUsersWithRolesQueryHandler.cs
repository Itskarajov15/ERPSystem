using ErpSystem.Application.Authentication.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Authentication.Queries.GetUsersWithRoles;

internal class GetUsersWithRolesQueryHandler
    : IRequestHandler<GetUsersWithRolesQuery, PageResult<UserWithRolesDto>>
{
    private readonly IRepository _repository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public GetUsersWithRolesQueryHandler(
        IRepository repository,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager
    )
    {
        _repository = repository;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<PageResult<UserWithRolesDto>> Handle(
        GetUsersWithRolesQuery request,
        CancellationToken cancellationToken
    )
    {
        var filterBy = ComposeFilterBy(request.UserFilters);

        var users = await _repository.GetPaginatedAsync<ApplicationUser, UserWithRolesDto>(
            request.PaginationParams,
            q =>
                q.Select(u => new UserWithRolesDto()
                {
                    Id = u.Id.ToString(),
                    UserName = u.UserName!,
                    Email = u.Email ?? string.Empty,
                    FirstName = u.FirstName ?? string.Empty,
                    LastName = u.LastName ?? string.Empty,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    LastLogin = u.LastLogin,
                }),
            filterBy
        );

        var userIds = users.Items.Select(u => Guid.Parse(u.Id)).ToList();

        var appUsers = await _repository
            .AllReadOnly<ApplicationUser>()
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync(cancellationToken);

        var appUserDict = appUsers.ToDictionary(u => u.Id, u => u);

        foreach (var userDto in users.Items)
        {
            if (
                Guid.TryParse(userDto.Id, out var userId)
                && appUserDict.TryGetValue(userId, out var appUser)
            )
            {
                var roles = await _userManager.GetRolesAsync(appUser);
                userDto.RoleName = roles.FirstOrDefault() ?? string.Empty;
            }
            else
            {
                userDto.RoleName = string.Empty;
            }
        }

        return users;
    }

    private Func<IQueryable<ApplicationUser>, IQueryable<ApplicationUser>> ComposeFilterBy(
        UserFilters? userFilters
    ) =>
        query =>
        {
            if (userFilters == null)
            {
                return query;
            }

            if (userFilters.SearchTerm != null)
            {
                var searchTermLowerCase = userFilters.SearchTerm.ToLower();

                query = query.Where(u =>
                    (u.FirstName + " " + u.LastName).ToLower().Contains(searchTermLowerCase)
                    || u.Email!.ToLower().Contains(searchTermLowerCase)
                );
            }

            return query;
        };
}
