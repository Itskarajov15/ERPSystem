namespace ErpSystem.Domain.Interfaces;

public interface IInventoryService
{
    Task IncreaseStockAsync(Guid productId, decimal quantity);

    Task IncreaseStockOfMultipleItemsAsync(IEnumerable<(Guid productId, int quantity)> items);

    Task DecreaseStockAsync(Guid productId, decimal quantity);

    Task<bool> HasSufficientStockAsync(Guid productId, decimal quantity);
}
