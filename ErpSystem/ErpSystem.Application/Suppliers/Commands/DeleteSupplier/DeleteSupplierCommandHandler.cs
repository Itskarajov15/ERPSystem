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
        var supplier = await _repository.GetByIdAsync<Supplier>(request.Id);

        if (supplier is null)
        {
            throw new NotFoundException(nameof(Supplier), request.Id);
        }

        _repository.SoftDelete<Supplier>(supplier);
        await _repository.SaveChangesAsync();
    }
}
