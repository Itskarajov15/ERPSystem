using MediatR;

namespace ErpSystem.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal UnitPrice,
    string Sku,
    decimal ReorderLevel,
    Guid UnitOfMeasureId
) : IRequest;
