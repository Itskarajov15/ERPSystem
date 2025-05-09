using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Infrastructure.Persistance.Repositories;

internal class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context)
        : base(context) { }

    public async Task<IReadOnlyList<Product>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken
    ) =>
        await _context
            .Products.Where(p => ids.Contains(p.Id) && !p.IsDeleted)
            .ToListAsync(cancellationToken);

    public async Task<(IReadOnlyList<Product>, int count)> GetProductsAsync(
        string? searchText,
        string? sortBy,
        bool ascending,
        bool onlyLowStock,
        int page,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        var query = _context.Products.Where(p => !p.IsDeleted).AsQueryable();

        if (onlyLowStock)
        {
            query = query.Where(p => p.Quantity < p.ReorderLevel);
        }

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            query = query.Where(p => p.Name.Contains(searchText));
        }

        var count = await query.CountAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            query = ascending
                ? query.OrderBy(p => EF.Property<object>(p, sortBy))
                : query.OrderByDescending(p => EF.Property<object>(p, sortBy));
        }

        var products = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (products, count);
    }
}
