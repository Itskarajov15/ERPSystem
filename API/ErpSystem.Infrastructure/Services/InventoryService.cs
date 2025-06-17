using ErpSystem.Application.Common.Constants;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ErpSystem.Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly IRepository _repository;

    public InventoryService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task IncreaseStockAsync(Guid productId, decimal quantity)
    {
        var product = await _repository.GetByIdAsync<Product>(productId);

        if (product == null)
        {
            throw new KeyNotFoundException(ProductErrorKeys.ProductNotFound);
        }

        if (quantity <= 0)
        {
            throw new ArgumentException(ProductErrorKeys.QuantityInvalid);
        }

        product.Quantity += (int)quantity;

        await _repository.SaveChangesAsync();
    }

    public async Task IncreaseStockOfMultipleItemsAsync(
        IEnumerable<(Guid productId, int quantity)> items
    )
    {
        if (!items.Any())
        {
            return;
        }

        var productIds = items.Select(i => i.productId).ToList();
        var products = await _repository.GetByIdsAsync<Product>(productIds);

        if (products.Count() != productIds.Count)
        {
            var missingProductIds = productIds.Except(products.Select(p => p.Id)).ToList();
        }

        foreach (var (productId, quantity) in items)
        {
            if (quantity <= 0)
            {
                continue;
            }

            var product = products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                product.Quantity += quantity;
            }
        }

        await _repository.SaveChangesAsync();
    }

    public async Task DecreaseStockAsync(Guid productId, decimal quantity)
    {
        var product = await _repository.GetByIdAsync<Product>(productId);

        if (product == null)
        {
            throw new KeyNotFoundException(ProductErrorKeys.ProductNotFound);
        }

        if (quantity <= 0)
        {
            throw new ArgumentException(ProductErrorKeys.QuantityInvalid);
        }

        var quantityInt = (int)quantity;

        if (product.Quantity < quantityInt)
        {
            throw new InvalidOperationException(ProductErrorKeys.InsufficientStock);
        }

        product.Quantity -= quantityInt;

        await _repository.SaveChangesAsync();
    }

    public async Task<bool> HasSufficientStockAsync(Guid productId, decimal quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException(ProductErrorKeys.QuantityInvalid);
        }

        var product = await _repository.GetByIdAsync<Product>(productId);

        if (product == null)
        {
            return false;
        }

        var available = product.Quantity - product.ReservedQuantity;
        var hasStock = available >= (int)quantity;

        return hasStock;
    }
}
