using MediatR;

namespace ErpSystem.Application.Products.Commands.AddProduct;

public record AddProductCommand(
    string Name,
    string Sku,
    string Description,
    decimal UnitPrice,
    int ReorderLevel,
    Guid UnitOfMeasureId
) : IRequest<Guid>;
