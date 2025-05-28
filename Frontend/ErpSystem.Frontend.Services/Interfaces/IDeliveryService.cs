using ErpSystem.Frontend.Core.Models.Deliveries;
using Microsoft.AspNetCore.Http;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface IDeliveryService
{
    Task<(List<DeliveryViewModel> Items, int TotalCount)> GetDeliveriesAsync(
        DeliveryFilterModel filter
    );

    Task<DeliveryViewModel?> GetDeliveryByIdAsync(Guid id);

    Task<Guid> CreateDeliveryAsync(DeliveryEditModel model);

    Task StartDeliveryProgressAsync(Guid id);

    Task CompleteDeliveryAsync(Guid id);

    Task DeleteDeliveryAsync(Guid id);

    Task<bool> UpdateDeliveryStatusAsync(int id, string status);

    Task<bool> UpdateDeliveryCommentAsync(int id, string comment);

    Task ExportDeliveriesAsync(DeliveryFilterModel filter);

    Task ImportPrioritiesAsync(IFormFile file);
}
