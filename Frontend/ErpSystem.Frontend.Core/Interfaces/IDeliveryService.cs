using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Deliveries;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface IDeliveryService
{
    Task<PageResult<DeliveryViewModel>> GetDeliveriesAsync(DeliveryFilterModel? filter = null);
    Task<DeliveryDetailViewModel?> GetDeliveryByIdAsync(Guid id);
    Task<Guid> CreateDeliveryAsync(DeliveryCreateModel model);
    Task StartDeliveryProgressAsync(Guid id);
    Task CompleteDeliveryAsync(Guid id);
    Task DeleteDeliveryAsync(Guid id);
}
