using ErpSystem.Application.Common.Constants;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Suppliers.Commands.DeleteSupplier;

internal class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand>
{
    private readonly IRepository _repository;

    public DeleteSupplierCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var deliveries = await _repository
            .AllReadOnly<Delivery>()
            .Where(d => d.SupplierId == request.Id)
            .ToListAsync(cancellationToken);

        if (deliveries.Any())
        {
            throw new InvalidOperationException(SupplierErrorKeys.SupplierExistsInDelivery);
        }

        await _repository.SoftDeleteById<Supplier>(request.Id);
        await _repository.SaveChangesAsync();
    }
}
