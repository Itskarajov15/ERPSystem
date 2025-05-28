using ErpSystem.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Sales;

public class PaymentMethodConfiguration : BaseEntityConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.HasKey(pm => pm.Id);

        builder.Property(pm => pm.Name).IsRequired().HasMaxLength(50);

        builder.HasQueryFilter(pm => !pm.IsDeleted);
    }
}
