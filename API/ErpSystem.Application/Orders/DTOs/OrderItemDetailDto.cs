namespace ErpSystem.Application.Orders.DTOs;

public class OrderItemDetailDto
{
    public Guid Id { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string ProductSku { get; set; } = string.Empty;

    public string UnitOfMeasure { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice => Quantity * UnitPrice;
}
