namespace ErpSystem.Domain.Interfaces;

public interface IInventoryService
{
    Task IncreaseStockAsync(int productId, decimal quantity);

    Task IncreaseStockOfMultipleItemsAsync(
        IEnumerable<(Guid productId, int quantity)> items,
        CancellationToken cancellationToken
    );

    Task DecreaseStockAsync(int productId, decimal quantity);

    Task<bool> HasSufficientStockAsync(int productId, decimal quantity);
}
