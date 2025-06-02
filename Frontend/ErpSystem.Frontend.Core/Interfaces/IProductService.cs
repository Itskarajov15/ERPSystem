using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Products;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface IProductService
{
    Task<PageResult<ProductViewModel>> GetProductsAsync(ProductFilterModel? filter = null);
    
    Task<ProductViewModel?> GetProductByIdAsync(Guid id);
    
    Task<Guid> AddProductAsync(ProductEditModel model);
    
    Task UpdateProductAsync(ProductEditModel model);
    
    Task DeleteProductAsync(Guid id);
}
