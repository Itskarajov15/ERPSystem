using AutoMapper;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Application.Orders.DTOs;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Orders.Queries.GetOrderDetails;

internal class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderDetailsQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderDetailDto> Handle(
        GetOrderDetailsQuery request,
        CancellationToken cancellationToken
    )
    {
        var order = await _orderRepository.GetByIdWithItemsAsync(request.Id, cancellationToken);
        if (order == null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }

        var orderDetailDto = _mapper.Map<OrderDetailDto>(order);

        return orderDetailDto;
    }
}
