using ErpSystem.Domain.Entities.Sales;

namespace ErpSystem.Application.Orders.DTOs;

public class OrderDto
{
    public Guid Id { get; set; }

    public DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; }

    public string StatusName => Status.ToString();

    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public Guid PaymentMethodId { get; set; }

    public string PaymentMethodName { get; set; } = string.Empty;

    public int TotalItems { get; set; }

    public decimal TotalAmount { get; set; }
}
