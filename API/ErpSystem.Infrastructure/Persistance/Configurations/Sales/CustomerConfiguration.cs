using ErpSystem.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Sales;

public class CustomerConfiguration : BaseEntityConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder.Property(c => c.ContactName).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Phone).IsRequired().HasMaxLength(15);

        builder.Property(c => c.Email).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Address).IsRequired().HasMaxLength(200);

        builder.HasQueryFilter(c => !c.IsDeleted);

        // Seed data
        builder.HasData(GetSeedData());
    }

    private static Customer[] GetSeedData()
    {
        return new[]
        {
            new Customer
            {
                Id = Guid.Parse("2BF41498-995A-4A13-8391-F6E0F5223F08"),
                Name = "Адвокатско Дружество Петров и Партньори",
                ContactName = "Стефан Василев",
                Phone = "+359889123456",
                Email = "stefan.vasilev@petrov-partners.bg",
                Address = "гр. София, бул. Витоша 128",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new Customer
            {
                Id = Guid.Parse("EB81D76E-D727-47EC-B4E5-AF9503BAA6FD"),
                Name = "Счетоводна Къща Баланс ЕООД",
                ContactName = "Анна Христова",
                Phone = "+359878234567",
                Email = "anna.hristova@balance-accounting.bg",
                Address = "гр. Пловдив, ул. Марица 89",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new Customer
            {
                Id = Guid.Parse("6C6B1118-9A9B-4BD2-BC19-933BDBE74710"),
                Name = "IT Солюшънс България ООД",
                ContactName = "Петър Атанасов",
                Phone = "+359867345678",
                Email = "petar.atanasov@it-solutions.bg",
                Address = "гр. Варна, ул. Приморски 12",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new Customer
            {
                Id = Guid.Parse("B98C77D0-6417-4791-A3EE-1ACF0D7E18CD"),
                Name = "Училище Христо Ботев",
                ContactName = "Румяна Кирилова",
                Phone = "+359856456789",
                Email = "rumyana.kirilova@school-botev.bg",
                Address = "гр. Бургас, бул. Демокрация 45",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new Customer
            {
                Id = Guid.Parse("F490AE0C-4ABE-42AC-B499-FED676BECADD"),
                Name = "Общинска Администрация Русе",
                ContactName = "Владимир Михайлов",
                Phone = "+359845567890",
                Email = "vladimir.mihaylov@ruse-municipality.bg",
                Address = "гр. Русе, ул. Дунав 78",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
        };
    }
}
