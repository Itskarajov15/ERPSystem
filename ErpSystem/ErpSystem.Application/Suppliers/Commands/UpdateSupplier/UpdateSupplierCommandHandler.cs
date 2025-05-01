using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Suppliers.Commands.UpdateSupplier;

internal class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSupplierCommandHandler(
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork
    )
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(request.Id, cancellationToken);

        if (supplier is null)
        {
            throw new NotFoundException(nameof(Supplier), request.Id);
        }

        supplier.Name = request.Name;
        supplier.Address = request.Address;
        supplier.Phone = request.Phone;
        supplier.Email = request.Email;
        supplier.ContactPerson = request.ContactPerson;

        _supplierRepository.Update(supplier);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
