namespace ErpSystem.Frontend.Core.Models.Deliveries;

public class DeliveryItemApiRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
