using AutoMapper;
using ErpSystem.Application.Common.Models;
using ErpSystem.Application.Orders.DTOs;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Orders.Queries.GetOrders;

internal class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PaginatedList<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OrderDto>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken
    )
    {
        var orders = await _orderRepository.GetOrdersAsync(
            request.Filters,
            request.PaginationRequest,
            cancellationToken
        );

        var orderDtos = _mapper.Map<List<OrderDto>>(orders);

        return new PaginatedList<OrderDto>(
            orderDtos,
            orders.Count,
            request.PaginationRequest.Page,
            request.PaginationRequest.PageSize
        );
    }
}
