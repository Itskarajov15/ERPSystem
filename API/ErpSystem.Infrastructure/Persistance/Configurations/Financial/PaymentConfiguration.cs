using ErpSystem.Domain.Entities.Financial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Financial;

public class PaymentConfiguration : BaseEntityConfiguration<Payment>
{
    public override void Configure(EntityTypeBuilder<Payment> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Amount).HasPrecision(18, 2).IsRequired();

        builder.Property(p => p.PaymentDate).IsRequired();

        builder.Property(p => p.PaymentReference).HasMaxLength(100);

        builder
            .HasOne(p => p.Invoice)
            .WithOne(i => i.Payment)
            .HasForeignKey<Payment>(p => p.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(p => p.PaymentMethod)
            .WithMany()
            .HasForeignKey(p => p.PaymentMethodId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
