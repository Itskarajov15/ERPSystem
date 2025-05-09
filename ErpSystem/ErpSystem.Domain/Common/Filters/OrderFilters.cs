using ErpSystem.Domain.Entities.Sales;

namespace ErpSystem.Domain.Common.Filters;

public class OrderFilters
{
    public Guid? CustomerId { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public OrderStatus? Status { get; set; }
}
