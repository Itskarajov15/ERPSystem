using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Suppliers;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface ISupplierService
{
    Task<PageResult<SupplierViewModel>> GetSuppliersAsync();
}
