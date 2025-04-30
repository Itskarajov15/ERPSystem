using ErpSystem.Application.Products.Queries.Common;
using MediatR;

namespace ErpSystem.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
