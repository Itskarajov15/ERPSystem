using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Products.Commands.AddProduct;

internal class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
{
    private readonly IRepository _repository;

    public AddProductCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var unitOfMeasure = await _repository.GetByIdAsync<UnitOfMeasure>(request.UnitOfMeasureId);

        if (unitOfMeasure is null)
        {
            throw new NotFoundException(nameof(UnitOfMeasure), request.UnitOfMeasureId);
        }

        var product = new Product
        {
            Name = request.Name,
            Sku = request.Sku,
            Description = request.Description,
            Price = request.UnitPrice,
            UnitOfMeasureId = request.UnitOfMeasureId,
            ReorderLevel = request.ReorderLevel,
        };

        await _repository.AddAsync<Product>(product);
        await _repository.SaveChangesAsync();

        return product.Id;
    }
}
