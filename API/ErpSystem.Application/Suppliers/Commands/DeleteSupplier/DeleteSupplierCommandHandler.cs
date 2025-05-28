using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using MediatR;

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
        await _repository.SoftDeleteById<Supplier>(request.Id);
        await _repository.SaveChangesAsync();
    }
}
