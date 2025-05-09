using ErpSystem.Domain.Abstractions;

namespace ErpSystem.Domain.Entities.Sales;

public class Order : BaseEntity
{
    public Guid CustomerId { get; set; }

    public Guid PaymentMethodId { get; set; }

    public DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; }

    public PaymentMethod PaymentMethod { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
}
