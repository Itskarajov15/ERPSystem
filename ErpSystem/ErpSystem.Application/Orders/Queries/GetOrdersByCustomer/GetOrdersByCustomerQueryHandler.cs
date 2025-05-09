using AutoMapper;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Application.Common.Models;
using ErpSystem.Application.Orders.DTOs;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Orders.Queries.GetOrdersByCustomer;

internal class GetOrdersByCustomerQueryHandler
    : IRequestHandler<GetOrdersByCustomerQuery, PaginatedList<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetOrdersByCustomerQueryHandler(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IMapper mapper
    )
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OrderDto>> Handle(
        GetOrdersByCustomerQuery request,
        CancellationToken cancellationToken
    )
    {
        var customer = await _customerRepository.GetByIdAsync(
            request.CustomerId,
            cancellationToken
        );

        if (customer is null)
        {
            throw new NotFoundException(nameof(Customer), request.CustomerId);
        }

        var orders = await _orderRepository.GetByCustomerIdAsync(
            request.CustomerId,
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
