using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Customers.Commands.UpdateCustomer;

internal class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IRepository _repository;

    public UpdateCustomerCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync<Customer>(request.Id);

        if (customer is null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        customer.Name = request.Name;
        customer.Address = request.Address;
        customer.Phone = request.Phone;
        customer.Email = request.Email;
        customer.ContactName = request.ContactName;

        await _repository.SaveChangesAsync();
    }
}
