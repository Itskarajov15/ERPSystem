using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Suppliers.Commands.AddSupplier;

internal class AddSupplierCommandHandler : IRequestHandler<AddSupplierCommand, Guid>
{
    private readonly IRepository _repository;

    public AddSupplierCommandHandler(IRepository repository)
    {
        _repository = repository;
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

        await _repository.AddAsync<Supplier>(supplier);
        await _repository.SaveChangesAsync();

        return supplier.Id;
    }
}
