using ErpSystem.Domain.Entities.Inventory;

namespace ErpSystem.Domain.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<(IReadOnlyList<Product>, int count)> GetProductsAsync(
        string? searchText,
        string? sortBy,
        bool ascending,
        bool onlyLowStock,
        int page,
        int pageSize,
        CancellationToken cancellationToken
    );
}
