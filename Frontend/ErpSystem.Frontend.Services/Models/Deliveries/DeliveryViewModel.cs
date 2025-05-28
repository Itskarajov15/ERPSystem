namespace ErpSystem.Frontend.Core.Models.Deliveries;

public class DeliveryViewModel
{
    public Guid Id { get; set; }

    public Guid SupplierId { get; set; }

    public string SupplierName { get; set; } = string.Empty;

    public string DeliveryNumber { get; set; } = string.Empty;

    public string DeliveryDate { get; set; } = string.Empty;

    public string? Comment { get; set; }

    public DeliveryStatus Status { get; set; }

    public string StatusName { get; set; } = string.Empty;

    public bool CanBeDeleted { get; set; }

    public List<DeliveryItemViewModel> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(i => i.TotalPrice);
}
