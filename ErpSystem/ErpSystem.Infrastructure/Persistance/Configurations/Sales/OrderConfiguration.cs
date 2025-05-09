using ErpSystem.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Sales;

public class OrderConfiguration : BaseEntityConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderDate).IsRequired();

        builder.Property(o => o.Status).IsRequired();

        builder.Property(o => o.Notes).HasMaxLength(500);

        builder
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(o => o.PaymentMethod)
            .WithMany()
            .HasForeignKey(o => o.PaymentMethodId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(o => !o.IsDeleted);
    }
}
