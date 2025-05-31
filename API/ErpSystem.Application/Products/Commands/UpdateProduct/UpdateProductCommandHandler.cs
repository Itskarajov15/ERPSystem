using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Products.Commands.UpdateProduct;

internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IRepository _repository;

    public UpdateProductCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync<Product>(request.Id);

        if (product is null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        var unitOfMeasure = await _repository.GetByIdAsync<UnitOfMeasure>(request.UnitOfMeasureId);

        if (unitOfMeasure is null)
        {
            throw new NotFoundException(nameof(UnitOfMeasure), request.UnitOfMeasureId);
        }

        product.Name = request.Name;
        product.Sku = request.Sku;
        product.Description = request.Description;
        product.Price = request.UnitPrice;
        product.ReorderLevel = request.ReorderLevel;
        product.UnitOfMeasureId = request.UnitOfMeasureId;

        await _repository.SaveChangesAsync();
    }
}
