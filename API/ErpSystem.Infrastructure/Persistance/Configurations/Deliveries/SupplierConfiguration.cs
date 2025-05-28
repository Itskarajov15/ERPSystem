using ErpSystem.Domain.Entities.Deliveries;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Deliveries;

public class SupplierConfiguration : BaseEntityConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);

        builder.Property(s => s.ContactPerson).IsRequired().HasMaxLength(100);

        builder.Property(s => s.Phone).IsRequired().HasMaxLength(15);

        builder.Property(s => s.Email).IsRequired().HasMaxLength(100);

        builder.Property(s => s.Address).IsRequired().HasMaxLength(200);

        builder.HasQueryFilter(s => !s.IsDeleted);
    }
}
