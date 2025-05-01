using ErpSystem.Application.Products.DTOs;
using MediatR;

namespace ErpSystem.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
