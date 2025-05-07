using AutoMapper;
using ErpSystem.Application.Common.Models;
using ErpSystem.Application.Deliveries.DTOs;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Deliveries.Queries.GetDeliveries;

internal class GetDeliveriesQueryHandler
    : IRequestHandler<GetDeliveriesQuery, PaginatedList<DeliveryDto>>
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IMapper _mapper;

    public GetDeliveriesQueryHandler(IDeliveryRepository deliveryRepository, IMapper mapper)
    {
        _deliveryRepository = deliveryRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<DeliveryDto>> Handle(
        GetDeliveriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var deliveries = await _deliveryRepository.GetDeliveriesAsync(
            request.Filter,
            request.PaginationRequest,
            cancellationToken
        );

        var deliveryDtos = _mapper.Map<List<DeliveryDto>>(deliveries);

        return new PaginatedList<DeliveryDto>(
            deliveryDtos,
            deliveryDtos.Count,
            request.PaginationRequest.Page,
            request.PaginationRequest.PageSize
        );
    }
}
