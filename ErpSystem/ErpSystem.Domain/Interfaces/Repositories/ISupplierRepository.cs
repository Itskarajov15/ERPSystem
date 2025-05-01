using ErpSystem.Domain.Entities.Deliveries;

namespace ErpSystem.Domain.Interfaces.Repositories;

public interface ISupplierRepository : IRepository<Supplier>
{
    IReadOnlyList<Supplier> GetSuppliersByName(string name, CancellationToken cancellationToken);
}
