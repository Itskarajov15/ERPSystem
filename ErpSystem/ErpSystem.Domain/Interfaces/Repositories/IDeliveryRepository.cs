using ErpSystem.Domain.Entities.Deliveries;

namespace ErpSystem.Domain.Interfaces.Repositories;

public interface IDeliveryRepository : IRepository<Delivery>
{
    Task<Delivery?> GetByIdWithItemsAsync(Guid id);

    Task<IReadOnlyList<Delivery>> GetBySupplierIdAsync(Guid supplierId);

    Task<IReadOnlyList<Delivery>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
}
