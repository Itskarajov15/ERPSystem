using ErpSystem.Domain.Entities.Sales;

namespace ErpSystem.Domain.Interfaces.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> GetByIdWithItemsAsync(Guid id);

    Task<IReadOnlyList<Order>> GetByCustomerIdAsync(Guid customerId);

    Task<IReadOnlyList<Order>> GetByStatusAsync(OrderStatus status);

    Task<IReadOnlyList<Order>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
}
