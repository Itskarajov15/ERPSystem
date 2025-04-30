using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Products.Commands.UpdateProduct;

internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfMeasureRepository unitOfMeasureRepository,
        IUnitOfWork unitOfWork
    )
    {
        _productRepository = productRepository;
        _unitOfMeasureRepository = unitOfMeasureRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        var unitOfMeasure = await _unitOfMeasureRepository.GetByIdAsync(
            request.UnitOfMeasureId,
            cancellationToken
        );

        if (unitOfMeasure is null)
        {
            throw new NotFoundException(nameof(UnitOfMeasure), request.UnitOfMeasureId);
        }

        product.Name = request.Name;
        product.Sku = request.Sku;
        product.Description = request.Description;
        product.Price = request.Price;
        product.ReorderLevel = request.ReorderLevel;
        product.UnitOfMeasureId = request.UnitOfMeasureId;

        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
