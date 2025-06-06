using System.ComponentModel.DataAnnotations.Schema;
using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Financial;

namespace ErpSystem.Domain.Entities.Sales;

public class Order : BaseEntity
{
    public Guid CustomerId { get; set; }

    public Guid PaymentMethodId { get; set; }

    public DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; }

    public string? Notes { get; set; }

    public Invoice? Invoice { get; set; }

    public Customer Customer { get; set; } = null!;

    public PaymentMethod PaymentMethod { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

    public bool CanGenerateInvoice() => Status == OrderStatus.Completed && Invoice == null;
}
