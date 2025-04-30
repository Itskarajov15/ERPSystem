using ErpSystem.Domain.Abstractions;

namespace ErpSystem.Domain.Entities.Sales;

public class PaymentMethod : BaseEntity
{
    public string Name { get; set; } = null!;
}
