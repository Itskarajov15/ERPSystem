namespace ErpSystem.Frontend.Core.Models.Orders;

public class OrderItemApiRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
} 