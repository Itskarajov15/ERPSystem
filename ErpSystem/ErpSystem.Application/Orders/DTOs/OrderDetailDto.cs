namespace ErpSystem.Application.Orders.DTOs;

public class OrderDetailDto
{
    public Guid Id { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerPhone { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    public string CustomerAddress { get; set; } = string.Empty;

    public Guid PaymentMethodId { get; set; }

    public string PaymentMethodName { get; set; } = string.Empty;

    public List<OrderItemDetailDto> Items { get; set; } = new();

    public int TotalItems => Items.Sum(i => i.Quantity);

    public decimal Subtotal => Items.Sum(i => i.TotalPrice);
}
