using ErpSystem.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Inventory;

public class UnitOfMeasureConfiguration : BaseEntityConfiguration<UnitOfMeasure>
{
    public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name).IsRequired().HasMaxLength(50);

        builder.HasQueryFilter(u => !u.IsDeleted);
    }
}
