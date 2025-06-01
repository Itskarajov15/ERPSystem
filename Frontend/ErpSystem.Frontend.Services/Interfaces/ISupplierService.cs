using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Suppliers;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface ISupplierService
{
    Task<PageResult<SupplierViewModel>> GetSuppliersAsync(SupplierFilterModel? filter = null);
    
    Task<SupplierViewModel?> GetSupplierByIdAsync(Guid id);
    
    Task<Guid> AddSupplierAsync(SupplierEditModel model);
    
    Task UpdateSupplierAsync(SupplierEditModel model);
    
    Task DeleteSupplierAsync(Guid id);
}
