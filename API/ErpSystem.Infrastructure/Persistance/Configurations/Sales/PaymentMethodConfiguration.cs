using ErpSystem.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Sales;

public class PaymentMethodConfiguration : BaseEntityConfiguration<PaymentMethod>
{
    public override void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        base.Configure(builder);

        builder.Property(pm => pm.Name).IsRequired().HasMaxLength(50);

        builder.HasQueryFilter(pm => !pm.IsDeleted);

        builder.HasData(GetSeedData());
    }

    private static PaymentMethod[] GetSeedData()
    {
        return new[]
        {
            new PaymentMethod
            {
                Id = Guid.Parse("B6525CE3-9252-4B5E-9098-84490C36C487"),
                Name = "В брой",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new PaymentMethod
            {
                Id = Guid.Parse("3D1CECAB-419F-4EC0-B2A6-A8260CD0D1E3"),
                Name = "Банков превод",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new PaymentMethod
            {
                Id = Guid.Parse("1643B266-EA12-4B44-A4F7-A96A41FEC121"),
                Name = "Карта",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
        };
    }
}
