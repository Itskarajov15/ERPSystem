namespace ErpSystem.Frontend.Core.Models.Orders;

public class OrderApiRequest
{
    public Guid CustomerId { get; set; }
    public Guid PaymentMethodId { get; set; }
    public string? Notes { get; set; }
    public List<OrderItemApiRequest> Items { get; set; } = new();
} 