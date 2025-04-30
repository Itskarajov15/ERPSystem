using MediatR;

namespace ErpSystem.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Sku,
    decimal ReorderLevel,
    Guid UnitOfMeasureId
) : IRequest;
