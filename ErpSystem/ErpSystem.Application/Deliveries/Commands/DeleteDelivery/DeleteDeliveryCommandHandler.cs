using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Deliveries.Commands.DeleteDelivery;

internal class DeleteDeliveryCommandHandler : IRequestHandler<DeleteDeliveryCommand>
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDeliveryCommandHandler(
        IDeliveryRepository deliveryRepository,
        IUnitOfWork unitOfWork
    )
    {
        _deliveryRepository = deliveryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteDeliveryCommand request, CancellationToken cancellationToken)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (delivery == null)
        {
            throw new NotFoundException(nameof(Delivery), request.Id);
        }

        if (delivery.CanBeDeleted())
        {
            throw new InvalidOperationException("Only registered deliveries can be deleted.");
        }

        _deliveryRepository.Delete(delivery);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
