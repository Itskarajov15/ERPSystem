using AutoMapper;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Application.Deliveries.DTOs;
using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Deliveries.Queries.GetDeliveryDetails;

internal class GetDeliveryDetailsQueryHandler
    : IRequestHandler<GetDeliveryDetailsQuery, DeliveryDto>
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetDeliveryDetailsQueryHandler(
        IDeliveryRepository deliveryRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _deliveryRepository = deliveryRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeliveryDto> Handle(
        GetDeliveryDetailsQuery request,
        CancellationToken cancellationToken
    )
    {
        var delivery = await _deliveryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (delivery == null)
        {
            throw new NotFoundException(nameof(Delivery), request.Id);
        }

        return _mapper.Map<DeliveryDto>(delivery);
    }
}
