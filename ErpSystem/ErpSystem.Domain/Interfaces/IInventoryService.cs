namespace ErpSystem.Domain.Interfaces;

public interface IInventoryService
{
    Task IncreaseStockAsync(
        Guid productId,
        decimal quantity,
        CancellationToken cancellationToken = default
    );

    Task IncreaseStockOfMultipleItemsAsync(
        IEnumerable<(Guid productId, int quantity)> items,
        CancellationToken cancellationToken
    );

    Task DecreaseStockAsync(
        Guid productId,
        decimal quantity,
        CancellationToken cancellationToken = default
    );

    Task<bool> HasSufficientStockAsync(
        Guid productId,
        decimal quantity,
        CancellationToken cancellationToken = default
    );
}
