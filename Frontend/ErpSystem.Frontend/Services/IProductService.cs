using ErpSystem.Frontend.Web.Models.Common;
using ErpSystem.Frontend.Web.Models.Products;

namespace ErpSystem.Frontend.Web.Services;

public interface IProductService
{
    Task<PagedResponse<ProductViewModel>> GetProductsAsync();
} 