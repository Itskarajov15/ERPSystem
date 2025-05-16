using ErpSystem.Application.Deliveries.DTOs;
using MediatR;

namespace ErpSystem.Application.Deliveries.Queries.GetDeliveryDetails;

public record GetDeliveryDetailsQuery(Guid Id) : IRequest<DeliveryDetailDto>;
