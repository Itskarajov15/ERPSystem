using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

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
        await _repository.SoftDeleteById<Customer>(request.Id);
        await _repository.SaveChangesAsync();
    }
}
