using ErpSystem.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Inventory;

public class ProductConfigurations : BaseEntityConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

        builder.Property(p => p.Sku).IsRequired().HasMaxLength(50);

        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);

        builder.Property(p => p.Quantity).IsRequired().HasDefaultValue(0);

        builder.Property(p => p.ReservedQuantity).IsRequired().HasDefaultValue(0);

        builder.Property(p => p.Price).HasPrecision(18, 2).IsRequired();

        builder.Property(p => p.ReorderLevel).HasPrecision(18, 2).IsRequired();

        builder
            .HasOne(p => p.UnitOfMeasure)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.UnitOfMeasureId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
