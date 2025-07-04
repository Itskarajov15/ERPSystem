﻿using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Deliveries.Commands.DeleteDelivery;

internal class DeleteDeliveryCommandHandler : IRequestHandler<DeleteDeliveryCommand>
{
    private readonly IRepository _repository;

    public DeleteDeliveryCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteDeliveryCommand request, CancellationToken cancellationToken)
    {
        var delivery = await _repository.GetByIdAsync<Delivery>(request.Id);

        if (delivery == null)
        {
            throw new NotFoundException(DeliveryErrorKeys.DeliveryNotFound, request.Id);
        }

        if (!delivery.CanBeDeleted())
        {
            throw new InvalidOperationException(DeliveryErrorKeys.DeliveryCannotBeDeleted);
        }

        _repository.SoftDelete(delivery);
        await _repository.SaveChangesAsync();
    }
}
