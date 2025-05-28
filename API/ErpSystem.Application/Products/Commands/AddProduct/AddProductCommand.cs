using MediatR;

namespace ErpSystem.Application.Products.Commands.AddProduct;

public record AddProductCommand(
    string Name,
    string Sku,
    string Description,
    decimal UnitPrice,
    decimal ReorderLevel,
    Guid UnitOfMeasureId
) : IRequest<Guid>;
