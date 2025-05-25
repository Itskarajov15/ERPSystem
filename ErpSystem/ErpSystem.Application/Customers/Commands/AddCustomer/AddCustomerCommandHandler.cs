using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Customers.Commands.AddCustomer;

internal class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Guid>
{
    private readonly IRepository _repository;

    public AddCustomerCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Name = request.Name,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email,
            ContactName = request.ContactName,
        };

        await _repository.AddAsync(customer);
        await _repository.SaveChangesAsync();

        return customer.Id;
    }
}
