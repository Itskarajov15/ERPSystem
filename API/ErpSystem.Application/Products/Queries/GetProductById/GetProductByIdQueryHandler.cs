using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Application.Products.DTOs;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Products.Queries.GetProductById;

internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IRepository _repository;

    public GetProductByIdQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductDto> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var product = await _repository
            .AllReadOnly<Product>()
            .Include(p => p.UnitOfMeasure)
            .FirstOrDefaultAsync(p => p.Id == request.Id);

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        return new ProductDto()
        {
            Id = request.Id,
            Name = product.Name,
            Sku = product.Sku,
            Description = product.Description,
            UnitOfMeasureId = product.UnitOfMeasureId,
            UnitOfMeasureName = product.UnitOfMeasure.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            ReorderLevel = product.ReorderLevel,
        };
    }
}
