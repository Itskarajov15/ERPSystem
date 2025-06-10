using ErpSystem.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Inventory;

public class UnitOfMeasureConfiguration : BaseEntityConfiguration<UnitOfMeasure>
{
    public override void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.Name).IsRequired().HasMaxLength(50);

        builder.HasQueryFilter(u => !u.IsDeleted);

        builder.HasData(GetSeedData());
    }

    private static UnitOfMeasure[] GetSeedData()
    {
        return new[]
        {
            new UnitOfMeasure
            {
                Id = Guid.Parse("FC720443-B659-4A2D-94B7-1CD4778B1040"),
                Name = "бр.",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new UnitOfMeasure
            {
                Id = Guid.Parse("E7D01A65-7F41-4560-90A8-7140C5FE3F6F"),
                Name = "кг",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
        };
    }
}
