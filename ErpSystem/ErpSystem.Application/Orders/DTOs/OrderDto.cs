namespace ErpSystem.Application.Orders.DTOs;

public class OrderDto
{
    public Guid Id { get; set; }

    public DateTime OrderDate { get; set; }

    public string StatusName { get; set; } = String.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public string PaymentMethodName { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }
}
