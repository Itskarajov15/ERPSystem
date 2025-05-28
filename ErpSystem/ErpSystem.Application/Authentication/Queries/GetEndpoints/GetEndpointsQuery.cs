using ErpSystem.Application.Authentication.DTOs;
using MediatR;

namespace ErpSystem.Application.Authentication.Queries.GetEndpoints;

public record GetEndpointsQuery() : IRequest<List<EndpointDto>>;
