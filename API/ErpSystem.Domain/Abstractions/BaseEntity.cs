namespace ErpSystem.Domain.Abstractions;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastModifiedAt { get; set; }

    public string? LastModifiedBy { get; set; }

    public bool IsDeleted { get; set; }
}
