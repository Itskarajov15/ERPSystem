using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Suppliers.Commands.AddSupplier;

internal class AddSupplierCommandHandler : IRequestHandler<AddSupplierCommand, Guid>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddSupplierCommandHandler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AddSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = new Supplier
        {
            Name = request.Name,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email,
            ContactPerson = request.ContactPerson,
        };

        _supplierRepository.Add(supplier);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return supplier.Id;
    }
}
