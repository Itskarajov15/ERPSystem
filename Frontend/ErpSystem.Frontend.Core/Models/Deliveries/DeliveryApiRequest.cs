namespace ErpSystem.Frontend.Core.Models.Deliveries;

public class DeliveryApiRequest
{
    public Guid SupplierId { get; set; }
    public string DeliveryNumber { get; set; } = string.Empty;
    public DateTime DeliveryDate { get; set; }
    public string? Comment { get; set; }
    public List<DeliveryItemApiRequest> Items { get; set; } = new();
}
