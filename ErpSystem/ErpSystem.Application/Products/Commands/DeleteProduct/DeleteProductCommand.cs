using MediatR;

namespace ErpSystem.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : IRequest;
