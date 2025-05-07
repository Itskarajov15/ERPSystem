using ErpSystem.Domain.Entities.Deliveries;

namespace ErpSystem.Domain.Interfaces.Repositories;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<IReadOnlyList<Supplier>> GetSuppliersByNameAsync(
        string name,
        CancellationToken cancellationToken
    );
}
