using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Products.Commands.DeleteProduct;

internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IRepository _repository;

    public DeleteProductCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync<Product>(request.ProductId);

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.ProductId);
        }

        await _repository.SaveChangesAsync();
    }
}
