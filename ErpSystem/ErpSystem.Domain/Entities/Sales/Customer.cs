using ErpSystem.Domain.Abstractions;

namespace ErpSystem.Domain.Entities.Sales;

public class Customer : BaseEntity
{
    public string Name { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}
