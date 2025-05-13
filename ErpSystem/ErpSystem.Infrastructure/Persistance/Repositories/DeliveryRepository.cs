using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Infrastructure.Persistance.Repositories;

public class DeliveryRepository : Repository<Delivery>, IDeliveryRepository
{
    public DeliveryRepository(ApplicationDbContext context)
        : base(context) { }

    public async Task<Delivery?> GetByIdWithItemsAsync(
        Guid id,
        CancellationToken cancellationToken
    ) =>
        await _context
            .Set<Delivery>()
            .Include(d => d.Supplier)
            .Include(d => d.DeliveryItems)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Delivery>> GetBySupplierIdAsync(
        Guid supplierId,
        CancellationToken cancellationToken
    ) =>
        await _context
            .Set<Delivery>()
            .Include(d => d.Supplier)
            .Include(d => d.DeliveryItems)
            .ThenInclude(i => i.Product)
            .Where(d => d.SupplierId == supplierId)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Delivery>> GetDeliveriesAsync(
        DeliveryFilters? filters,
        PaginationRequest paginationRequest,
        CancellationToken cancellationToken = default
    )
    {
        var query = _context.Set<Delivery>().AsQueryable();

        if (filters != null)
        {
            if (filters.SupplierId != null)
            {
                query = query.Where(d => d.SupplierId == filters.SupplierId);
            }
            if (filters.FromDate != null)
            {
                query = query.Where(d => d.DeliveryDate >= filters.FromDate);
            }
            if (filters.ToDate != null)
            {
                query = query.Where(d => d.DeliveryDate <= filters.ToDate);
            }
        }

        query = query
            .Include(d => d.Supplier)
            .Include(d => d.DeliveryItems)
            .ThenInclude(i => i.Product);

        query = ApplySorting(query, paginationRequest);

        return await query
            .Skip(paginationRequest.PageSize * paginationRequest.Page)
            .Take(paginationRequest.PageSize)
            .ToListAsync(cancellationToken);
    }

    private static IQueryable<Delivery> ApplySorting(
        IQueryable<Delivery> query,
        PaginationRequest paginationRequest
    )
    {
        return (paginationRequest.SortBy?.ToLower()) switch
        {
            "date" => paginationRequest.Ascending
                ? query.OrderBy(d => d.DeliveryDate)
                : query.OrderByDescending(d => d.DeliveryDate),
            "supplier" => paginationRequest.Ascending
                ? query.OrderBy(d => d.Supplier.Name)
                : query.OrderByDescending(d => d.Supplier.Name),
            "status" => paginationRequest.Ascending
                ? query.OrderBy(d => d.DeliveryStatus)
                : query.OrderByDescending(d => d.DeliveryStatus),
            _ => paginationRequest.Ascending
                ? query.OrderBy(d => d.DeliveryDate)
                : query.OrderByDescending(d => d.DeliveryDate),
        };
    }
}
