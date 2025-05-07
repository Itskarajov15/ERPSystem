using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Deliveries;

namespace ErpSystem.Domain.Interfaces.Repositories;

public interface IDeliveryRepository : IRepository<Delivery>
{
    Task<Delivery?> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Delivery>> GetBySupplierIdAsync(
        Guid supplierId,
        CancellationToken cancellationToken
    );

    Task<IReadOnlyList<Delivery>> GetByDateRangeAsync(
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken
    );

    Task<IReadOnlyList<Delivery>> GetDeliveriesAsync(
        DeliveryFilters? filters,
        PaginationRequest paginationRequest,
        CancellationToken cancellationToken = default
    );
}
