using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ErpSystem.Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly IRepository _repository;
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(IRepository repository, ILogger<InventoryService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task IncreaseStockAsync(
        Guid productId,
        decimal quantity,
        CancellationToken cancellationToken = default
    )
    {
        var product = await _repository.GetByIdAsync<Product>(productId);

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

        await _repository.SaveChangesAsync();

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
        var products = await _repository.GetByIdsAsync<Product>(productIds);

        if (products.Count() != productIds.Count)
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

        await _repository.SaveChangesAsync();
    }

    public async Task DecreaseStockAsync(
        Guid productId,
        decimal quantity,
        CancellationToken cancellationToken = default
    )
    {
        var product = await _repository.GetByIdAsync<Product>(productId);

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

        await _repository.SaveChangesAsync();

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

        var product = await _repository.GetByIdAsync<Product>(productId);

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
