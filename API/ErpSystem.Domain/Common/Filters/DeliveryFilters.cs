using ErpSystem.Domain.Entities.Deliveries;

namespace ErpSystem.Domain.Common.Filters;

public class DeliveryFilters
{
    public Guid? SupplierId { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public DeliveryStatus? Status { get; set; }
}
