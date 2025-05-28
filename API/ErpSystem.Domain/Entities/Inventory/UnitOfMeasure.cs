using ErpSystem.Domain.Abstractions;

namespace ErpSystem.Domain.Entities.Inventory;

public class UnitOfMeasure : BaseEntity
{
    public string Name { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}
