namespace ErpSystem.Frontend.Core.Models.Deliveries;

public class DeliveryDetailViewModel
{
    public Guid Id { get; set; }
    public string DeliveryNumber { get; set; } = string.Empty;
    public Guid SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string DeliveryDate { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public DeliveryStatus Status { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public bool CanBeDeleted { get; set; }
    public bool CanBeStarted { get; set; }
    public bool CanBeCompleted { get; set; }
    public string CreatedOn { get; set; } = string.Empty;
    public List<DeliveryItemDetailViewModel> Items { get; set; } = new();
    public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
}
