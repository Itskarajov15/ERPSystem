using MediatR;

namespace ErpSystem.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal UnitPrice,
    string Sku,
    int ReorderLevel,
    Guid UnitOfMeasureId
) : IRequest;
