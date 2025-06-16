using ErpSystem.Application.Common.Constants;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Customers.Commands.DeleteCustomer;

internal class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly IRepository _repository;

    public DeleteCustomerCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var orders = await _repository
            .AllReadOnly<Order>()
            .Where(o => o.CustomerId == request.Id)
            .ToListAsync(cancellationToken);

        if (orders.Any())
        {
            throw new InvalidOperationException(CustomerErrorKeys.CustomerExistsInOrder);
        }

        await _repository.SoftDeleteById<Customer>(request.Id);
        await _repository.SaveChangesAsync();
    }
}
