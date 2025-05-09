using ErpSystem.Application.Orders.DTOs;
using MediatR;

namespace ErpSystem.Application.Orders.Queries.GetOrderDetails;

public record GetOrderDetailsQuery(Guid Id) : IRequest<OrderDetailDto>;
