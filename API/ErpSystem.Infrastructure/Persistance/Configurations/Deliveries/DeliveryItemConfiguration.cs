using ErpSystem.Domain.Entities.Deliveries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Deliveries;

public class DeliveryItemConfiguration : BaseEntityConfiguration<DeliveryItem>
{
    public override void Configure(EntityTypeBuilder<DeliveryItem> builder)
    {
        base.Configure(builder);

        builder.Property(di => di.Quantity).IsRequired();

        builder.Property(di => di.UnitPrice).HasPrecision(18, 2).IsRequired();

        builder
            .HasOne(di => di.Product)
            .WithMany(p => p.DeliveryItems)
            .HasForeignKey(di => di.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(di => di.Delivery)
            .WithMany(d => d.DeliveryItems)
            .HasForeignKey(di => di.DeliveryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(di => !di.IsDeleted);
    }
}
