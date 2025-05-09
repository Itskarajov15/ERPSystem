using ErpSystem.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Sales;

public class CustomerConfiguration : BaseEntityConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder.Property(c => c.ContactName).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Phone).IsRequired().HasMaxLength(15);

        builder.Property(c => c.Email).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Address).IsRequired().HasMaxLength(200);

        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}
