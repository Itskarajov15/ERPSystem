using ErpSystem.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Inventory;

public class ProductConfigurations : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

        builder.Property(p => p.Sku).IsRequired().HasMaxLength(50);

        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);

        builder.Property(p => p.Quantity).IsRequired().HasDefaultValue(0);

        builder.Property(p => p.ReservedQuantity).IsRequired().HasDefaultValue(0);

        builder.Property(p => p.Price).HasPrecision(18, 2).IsRequired();

        builder.Property(p => p.ReorderLevel).IsRequired().HasDefaultValue(0);

        builder
            .HasOne(p => p.UnitOfMeasure)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.UnitOfMeasureId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.HasData(GetSeedData());
    }

    private static Product[] GetSeedData()
    {
        return new[]
        {
            new Product
            {
                Id = Guid.Parse("6EE1718F-A1EE-4BFF-8AB1-9CE688D14E4A"),
                Name = "Офис стол ергономичен",
                Sku = "CHAIR-ERG-001",
                Description =
                    "Ергономичен офис стол с регулируема височина, поддръжка за гръб и подлакътници",
                Quantity = 50,
                ReservedQuantity = 0,
                Price = 289.50m,
                ReorderLevel = 15,
                UnitOfMeasureId = Guid.Parse("FC720443-B659-4A2D-94B7-1CD4778B1040"), // бр.
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new Product
            {
                Id = Guid.Parse("980FCB9F-F9F1-4D9C-93CC-FE35B7E48B32"),
                Name = "Копирна хартия А4 80g",
                Sku = "PAPER-A4-80G",
                Description = "Висококачествена копирна хартия А4, 80g/m², 500 листа в пакет",
                Quantity = 200,
                ReservedQuantity = 0,
                Price = 12.99m,
                ReorderLevel = 50,
                UnitOfMeasureId = Guid.Parse("E7D01A65-7F41-4560-90A8-7140C5FE3F6F"), // кг
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new Product
            {
                Id = Guid.Parse("4007B57F-AFDB-45CC-9EFF-B66B0F31B154"),
                Name = "Химикалки сини комплект",
                Sku = "PEN-BLUE-SET10",
                Description = "Комплект от 10 сини химикалки за ежедневно писане, гладко писане",
                Quantity = 120,
                ReservedQuantity = 0,
                Price = 8.50m,
                ReorderLevel = 30,
                UnitOfMeasureId = Guid.Parse("FC720443-B659-4A2D-94B7-1CD4778B1040"), // бр.
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new Product
            {
                Id = Guid.Parse("3805CAB6-332F-4909-8546-679E0B2F2033"),
                Name = "Папки класьори А4",
                Sku = "FOLDER-A4-ARCH",
                Description = "Картонени класьори за документи А4, с метални скоби",
                Quantity = 80,
                ReservedQuantity = 0,
                Price = 4.75m,
                ReorderLevel = 25,
                UnitOfMeasureId = Guid.Parse("FC720443-B659-4A2D-94B7-1CD4778B1040"), // бр.
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
        };
    }
}
