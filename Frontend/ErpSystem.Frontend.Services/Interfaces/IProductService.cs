using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Products;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface IProductService
{
    Task<PagedResponse<ProductViewModel>> GetProductsAsync();
}
