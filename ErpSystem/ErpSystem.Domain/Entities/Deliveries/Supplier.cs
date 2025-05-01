using ErpSystem.Domain.Abstractions;

namespace ErpSystem.Domain.Entities.Deliveries;

public class Supplier : BaseEntity
{
    public string Name { get; set; } = null!;

    public string ContactPerson { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public ICollection<Delivery> Deliveries { get; set; } = new HashSet<Delivery>();
}
