using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Infrastructure.Persistance.Repositories;

internal class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context)
        : base(context) { }

    public async Task<IReadOnlyList<Order>> GetByCustomerIdAsync(
        Guid customerId,
        PaginationRequest paginationRequest,
        CancellationToken cancellationToken
    )
    {
        var query = _context
            .Orders.Where(o => o.CustomerId == customerId && !o.IsDeleted)
            .AsQueryable();

        var count = await query.CountAsync(cancellationToken);

        var orders = await query
            .Skip((paginationRequest.Page - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .ToListAsync(cancellationToken);

        return orders;
    }

    public async Task<Order?> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken) =>
        await _context
            .Orders.Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted, cancellationToken);

    public async Task<IReadOnlyList<Order>> GetOrdersAsync(
        OrderFilters? filters,
        PaginationRequest paginationRequest,
        CancellationToken cancellationToken = default
    )
    {
        var query = _context.Orders.Where(o => !o.IsDeleted).AsQueryable();

        if (filters is not null)
        {
            if (filters.CustomerId.HasValue)
            {
                query = query.Where(o => o.CustomerId == filters.CustomerId);
            }
            if (filters.FromDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= filters.FromDate);
            }
            if (filters.ToDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= filters.ToDate);
            }
            if (filters.Status.HasValue)
            {
                query = query.Where(o => o.Status == filters.Status);
            }
        }

        query = query
            .Include(o => o.Customer)
            .Include(o => o.PaymentMethod)
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.Product);

        query = ApplySorting(query, paginationRequest);

        return await query
            .Skip((paginationRequest.Page - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .ToListAsync(cancellationToken);
    }

    private static IQueryable<Order> ApplySorting(
        IQueryable<Order> query,
        PaginationRequest paginationRequest
    )
    {
        return (paginationRequest.SortBy?.ToLower()) switch
        {
            "date" => paginationRequest.Ascending
                ? query.OrderBy(o => o.OrderDate)
                : query.OrderByDescending(o => o.OrderDate),
            "customer" => paginationRequest.Ascending
                ? query.OrderBy(o => o.Customer.Name)
                : query.OrderByDescending(o => o.Customer.Name),
            "status" => paginationRequest.Ascending
                ? query.OrderBy(o => o.Status)
                : query.OrderByDescending(o => o.Status),
            _ => paginationRequest.Ascending
                ? query.OrderBy(o => o.OrderDate)
                : query.OrderByDescending(o => o.OrderDate),
        };
    }
}
