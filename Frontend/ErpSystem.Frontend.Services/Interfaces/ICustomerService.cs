using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Customers;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface ICustomerService
{
    Task<PageResult<CustomerViewModel>> GetCustomersAsync(CustomerFilterModel? filter = null);
    Task<CustomerViewModel?> GetCustomerByIdAsync(Guid id);
    Task<Guid> AddCustomerAsync(CustomerEditModel model);
    Task UpdateCustomerAsync(CustomerEditModel model);
    Task DeleteCustomerAsync(Guid id);
} 