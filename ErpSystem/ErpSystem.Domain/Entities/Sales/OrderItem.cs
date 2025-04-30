using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Inventory;

namespace ErpSystem.Domain.Entities.Sales;

public class OrderItem : BaseEntity
{
    public Guid ProductId { get; set; }

    public Guid OrderId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public Product Product { get; set; } = null!;

    public Order Order { get; set; } = null!;
}
