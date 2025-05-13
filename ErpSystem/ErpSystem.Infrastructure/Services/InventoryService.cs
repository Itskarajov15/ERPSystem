using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Interfaces;
using ErpSystem.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace ErpSystem.Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        ILogger<InventoryService> logger
    )
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task IncreaseStockAsync(
        Guid productId,
        decimal quantity,
        CancellationToken cancellationToken = default
    )
    {
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);

        if (product == null)
        {
            _logger.LogWarning("Cannot increase stock: Product {ProductId} not found", productId);
            throw new KeyNotFoundException($"Product with ID {productId} not found");
        }

        if (quantity <= 0)
        {
            _logger.LogWarning("Cannot increase stock: Quantity must be greater than zero");
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
        }

        product.Quantity += (int)quantity;
        _productRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Increased stock for product {ProductId} by {Quantity}",
            productId,
            quantity
        );
    }

    public async Task IncreaseStockOfMultipleItemsAsync(
        IEnumerable<(Guid productId, int quantity)> items,
        CancellationToken cancellationToken
    )
    {
        if (!items.Any())
        {
            _logger.LogWarning("Cannot increase stock: No items provided");
            return;
        }

        var productIds = items.Select(i => i.productId).ToList();
        var products = await _productRepository.GetByIdsAsync(productIds, cancellationToken);

        if (products.Count != productIds.Count)
        {
            var missingProductIds = productIds.Except(products.Select(p => p.Id)).ToList();
            _logger.LogWarning(
                "Some products were not found: {MissingProductIds}",
                string.Join(", ", missingProductIds)
            );
        }

        foreach (var (productId, quantity) in items)
        {
            if (quantity <= 0)
            {
                _logger.LogWarning(
                    "Skipping product {ProductId}: Quantity must be greater than zero",
                    productId
                );
                continue;
            }

            var product = products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                product.Quantity += quantity;
                _productRepository.Update(product);

                _logger.LogInformation(
                    "Increased stock for product {ProductId} by {Quantity}",
                    productId,
                    quantity
                );
            }
            else
            {
                _logger.LogWarning(
                    "Cannot increase stock: Product {ProductId} not found",
                    productId
                );
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DecreaseStockAsync(
        Guid productId,
        decimal quantity,
        CancellationToken cancellationToken = default
    )
    {
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);

        if (product == null)
        {
            _logger.LogWarning("Cannot decrease stock: Product {ProductId} not found", productId);
            throw new KeyNotFoundException($"Product with ID {productId} not found");
        }

        if (quantity <= 0)
        {
            _logger.LogWarning("Cannot decrease stock: Quantity must be greater than zero");
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
        }

        var quantityInt = (int)quantity;

        if (product.Quantity < quantityInt)
        {
            _logger.LogWarning(
                "Cannot decrease stock: Insufficient stock for product {ProductId}",
                productId
            );
            throw new InvalidOperationException(
                $"Insufficient stock for product {productId}. Available: {product.Quantity}, Requested: {quantityInt}"
            );
        }

        product.Quantity -= quantityInt;
        _productRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Decreased stock for product {ProductId} by {Quantity}",
            productId,
            quantity
        );
    }

    public async Task<bool> HasSufficientStockAsync(
        Guid productId,
        decimal quantity,
        CancellationToken cancellationToken = default
    )
    {
        if (quantity <= 0)
        {
            _logger.LogWarning("Invalid quantity check: Quantity must be greater than zero");
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
        }

        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);

        if (product == null)
        {
            _logger.LogWarning("Stock check failed: Product {ProductId} not found", productId);
            return false;
        }

        var available = product.Quantity - product.ReservedQuantity;
        var hasStock = available >= (int)quantity;

        _logger.LogInformation(
            "Stock check for product {ProductId}: Available={Available}, Required={Required}, Sufficient={HasStock}",
            productId,
            available,
            quantity,
            hasStock
        );

        return hasStock;
    }
}
