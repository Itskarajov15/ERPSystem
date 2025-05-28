namespace ErpSystem.Frontend.Web.Models.Deliveries;

public class DeliveryFilterModel
{
    public Guid? SupplierId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public DeliveryStatus? Status { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
} 