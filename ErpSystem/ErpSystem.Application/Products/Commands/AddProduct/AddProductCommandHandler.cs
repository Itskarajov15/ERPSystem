using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Products.Commands.AddProduct;

internal class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfMeasureRepository unitOfMeasureRepository,
        IUnitOfWork unitOfWork
    )
    {
        _productRepository = productRepository;
        _unitOfMeasureRepository = unitOfMeasureRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var unitOfMeasure = await _unitOfMeasureRepository.GetByIdAsync(
            request.UnitOfMeasureId,
            cancellationToken
        );

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

        _productRepository.Add(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
