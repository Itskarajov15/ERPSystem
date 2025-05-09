using ErpSystem.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations;

public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : BaseEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CreatedAt).IsRequired();

        builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(50);

        builder.Property(e => e.LastModifiedBy).HasMaxLength(50);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
