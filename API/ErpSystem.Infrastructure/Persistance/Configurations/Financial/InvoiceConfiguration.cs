using ErpSystem.Domain.Entities.Financial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Financial;

public class InvoiceConfiguration : BaseEntityConfiguration<Invoice>
{
    public override void Configure(EntityTypeBuilder<Invoice> builder)
    {
        base.Configure(builder);

        builder.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(50);

        builder.HasIndex(i => i.InvoiceNumber).IsUnique();

        builder.Property(i => i.InvoiceDate).IsRequired();

        builder.Property(i => i.Status).IsRequired();

        builder.Property(i => i.SubTotal).HasPrecision(18, 2).IsRequired();

        builder.Property(i => i.VatAmount).HasPrecision(18, 2).IsRequired();

        builder.Property(i => i.TotalAmount).HasPrecision(18, 2).IsRequired();

        builder.Property(i => i.Notes).HasMaxLength(500);

        builder
            .HasOne(i => i.Order)
            .WithOne(o => o.Invoice)
            .HasForeignKey<Invoice>(i => i.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(i => i.Customer)
            .WithMany()
            .HasForeignKey(i => i.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(i => !i.IsDeleted);
    }
}
