using ErpSystem.Application.Authentication.DTOs;
using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Authentication.Queries.GetEndpoints;

internal class GetEndpointsQueryHandler : IRequestHandler<GetEndpointsQuery, List<EndpointDto>>
{
    private readonly IRepository _repository;

    public GetEndpointsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<EndpointDto>> Handle(
        GetEndpointsQuery request,
        CancellationToken cancellationToken
    )
    {
        var routePermissions = await _repository
            .AllReadOnly<RoutePermission>()
            .Select(rp => new EndpointDto
            {
                Id = rp.Id.ToString(),
                ActionName = rp.ActionName,
                ControllerName = rp.ControllerName,
                Endpoint = $"/{rp.ControllerName}/{rp.ActionName}",
            })
            .ToListAsync(cancellationToken);

        return routePermissions;
    }
}
