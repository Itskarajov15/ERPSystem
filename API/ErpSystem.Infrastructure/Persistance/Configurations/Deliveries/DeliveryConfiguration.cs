using ErpSystem.Domain.Entities.Deliveries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Deliveries;

public class DeliveryConfiguration : BaseEntityConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.DeliveryDate).IsRequired();

        builder.Property(d => d.DeliveryNumber).HasMaxLength(50).IsRequired();

        builder.Property(d => d.Comment).HasMaxLength(200);

        builder.Property(d => d.DeliveryStatus).IsRequired();

        builder
            .HasOne(d => d.Supplier)
            .WithMany(s => s.Deliveries)
            .HasForeignKey(d => d.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(d => !d.IsDeleted);
    }
}
