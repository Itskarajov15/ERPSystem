using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Orders;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface IOrderService
{
    Task<PageResult<OrderViewModel>> GetOrdersAsync(OrderFilterModel? filter = null);
    Task<OrderDetailViewModel?> GetOrderByIdAsync(Guid id);
    Task<Guid> AddOrderAsync(OrderCreateModel model);
    Task CompleteOrderAsync(Guid id);
    Task CancelOrderAsync(Guid id);
    Task DeleteOrderAsync(Guid id);
}
