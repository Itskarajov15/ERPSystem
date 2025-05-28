using ErpSystem.Application.Authentication.DTOs;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Domain.Interfaces;
using MediatR;

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
        var roles = await _repository.GetPaginatedAsync<ApplicationRole, RoleDto>(
            request.PaginationParams,
            query =>
                query.Select(r => new RoleDto
                {
                    Id = r.Id.ToString(),
                    Name = r.Name!,
                    Description = r.Description,
                })
        );

        return roles;
    }
}
