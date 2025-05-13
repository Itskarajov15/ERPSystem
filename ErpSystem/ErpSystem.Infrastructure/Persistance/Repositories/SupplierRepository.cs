using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Infrastructure.Persistance.Repositories;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    public SupplierRepository(ApplicationDbContext context)
        : base(context) { }

    public async Task<IReadOnlyList<Supplier>> GetSuppliersByNameAsync(
        string name,
        CancellationToken cancellationToken
    ) =>
        await _context
            .Set<Supplier>()
            .Where(s => s.Name.Contains(name))
            .ToListAsync(cancellationToken);
}
