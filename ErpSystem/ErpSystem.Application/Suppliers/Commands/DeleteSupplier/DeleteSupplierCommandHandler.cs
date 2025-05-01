using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Suppliers.Commands.DeleteSupplier;

internal class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSupplierCommandHandler(
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork
    )
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(request.Id, cancellationToken);

        if (supplier is null)
        {
            throw new NotFoundException(nameof(Supplier), request.Id);
        }

        _supplierRepository.Delete(supplier);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
