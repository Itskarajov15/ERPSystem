using ErpSystem.Frontend.Web.Models.Common;
using ErpSystem.Frontend.Web.Models.Suppliers;

namespace ErpSystem.Frontend.Web.Services;

public interface ISupplierService
{
    Task<PagedResponse<SupplierViewModel>> GetSuppliersAsync();
} 