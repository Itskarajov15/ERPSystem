namespace ErpSystem.Infrastructure.Models;

public abstract class BaseClass
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }
}
