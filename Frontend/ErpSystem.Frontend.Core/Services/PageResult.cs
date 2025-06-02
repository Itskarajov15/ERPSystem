using ErpSystem.Frontend.Core.Models.Deliveries;

namespace ErpSystem.Frontend.Core.Services;

public partial class DeliveryService
{
    private class PageResult
    {
        public List<DeliveryViewModel> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
