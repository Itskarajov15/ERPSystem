using ErpSystem.Domain.Entities.Financial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Financial;

public class InvoiceItemConfiguration : BaseEntityConfiguration<InvoiceItem>
{
    public override void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        base.Configure(builder);

        builder.Property(ii => ii.ProductName).IsRequired().HasMaxLength(200);

        builder.Property(ii => ii.ProductSku).IsRequired().HasMaxLength(50);

        builder.Property(ii => ii.Quantity).IsRequired();

        builder.Property(ii => ii.UnitPrice).HasPrecision(18, 2).IsRequired();

        builder.Property(ii => ii.VatRate).HasPrecision(5, 2).IsRequired();

        builder.Property(ii => ii.VatAmount).HasPrecision(18, 2).IsRequired();

        builder.Property(ii => ii.LineTotal).HasPrecision(18, 2).IsRequired();

        builder
            .HasOne(ii => ii.Invoice)
            .WithMany(i => i.InvoiceItems)
            .HasForeignKey(ii => ii.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(ii => ii.Product)
            .WithMany()
            .HasForeignKey(ii => ii.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(ii => !ii.IsDeleted);
    }
}
