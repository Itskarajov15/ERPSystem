using ErpSystem.Domain.Entities.Sales;

namespace ErpSystem.Domain.Interfaces.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Order>> GetByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken
    );

    Task<IReadOnlyList<Order>> GetByStatusAsync(
        OrderStatus status,
        CancellationToken cancellationToken
    );

    Task<IReadOnlyList<Order>> GetByDateRangeAsync(
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken
    );
}
