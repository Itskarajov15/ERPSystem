using ErpSystem.Domain.Entities.Inventory;

namespace ErpSystem.Domain.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetBySkuAsync(string sku);

    Task<IReadOnlyList<Product>> GetProductsBelowReorderLevelAsync();
}
