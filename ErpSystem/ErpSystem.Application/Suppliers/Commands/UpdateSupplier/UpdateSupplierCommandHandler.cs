using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Suppliers.Commands.UpdateSupplier;

internal class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand>
{
    private readonly IRepository _repository;

    public UpdateSupplierCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _repository.GetByIdAsync<Supplier>(request.Id);

        if (supplier is null)
        {
            throw new NotFoundException(nameof(Supplier), request.Id);
        }

        supplier.Name = request.Name;
        supplier.Address = request.Address;
        supplier.Phone = request.Phone;
        supplier.Email = request.Email;
        supplier.ContactPerson = request.ContactPerson;

        await _repository.SaveChangesAsync();
    }
}
