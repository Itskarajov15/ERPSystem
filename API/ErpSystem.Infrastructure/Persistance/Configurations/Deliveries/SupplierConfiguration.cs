using ErpSystem.Domain.Entities.Deliveries;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Deliveries;

public class SupplierConfiguration : BaseEntityConfiguration<Supplier>
{
    public override void Configure(EntityTypeBuilder<Supplier> builder)
    {
        base.Configure(builder);

        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);

        builder.Property(s => s.ContactPerson).IsRequired().HasMaxLength(100);

        builder.Property(s => s.Phone).IsRequired().HasMaxLength(15);

        builder.Property(s => s.Email).IsRequired().HasMaxLength(100);

        builder.Property(s => s.Address).IsRequired().HasMaxLength(200);

        builder.HasQueryFilter(s => !s.IsDeleted);

        // Seed data
        builder.HasData(GetSeedData());
    }

    private static Supplier[] GetSeedData()
    {
        return new[]
        {
            new Supplier
            {
                Id = Guid.Parse("F742A28E-B0E0-4C50-BF4F-BF295EBAA9DD"),
                Name = "Офис Експрес ЕООД",
                ContactPerson = "Иван Петров",
                Phone = "+359888123456",
                Email = "ivan.petrov@office-express.bg",
                Address = "гр. София, ул. Витоша 15",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new Supplier
            {
                Id = Guid.Parse("07ECF61B-33CD-430B-BE4E-895DA076F73E"),
                Name = "Канцеларски Материали БГ АД",
                ContactPerson = "Мария Георгиева",
                Phone = "+359877234567",
                Email = "maria.georgieva@office-materials.bg",
                Address = "гр. Пловдив, бул. Русия 45",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new Supplier
            {
                Id = Guid.Parse("D2E467F9-C4C5-4641-B535-620566E37D8B"),
                Name = "Професионален Офис ООД",
                ContactPerson = "Георги Стоянов",
                Phone = "+359866345678",
                Email = "georgi.stoyanov@pro-office.bg",
                Address = "гр. Варна, ул. Васил Левски 23",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new Supplier
            {
                Id = Guid.Parse("50C8493B-76AA-4B61-AE9D-6EB69CFAF785"),
                Name = "Стейшънъри Груп ЕООД",
                ContactPerson = "Елена Димитрова",
                Phone = "+359855456789",
                Email = "elena.dimitrova@stationery-group.bg",
                Address = "гр. Бургас, ул. Александровска 67",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
            new Supplier
            {
                Id = Guid.Parse("FACCA2C6-49FF-471A-A853-8796EA574217"),
                Name = "Офис Мебели и Техника АД",
                ContactPerson = "Николай Тодоров",
                Phone = "+359844567890",
                Email = "nikolay.todorov@office-furniture.bg",
                Address = "гр. Стара Загора, ул. Гурко 34",
                CreatedBy = "system",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
            },
        };
    }
}
