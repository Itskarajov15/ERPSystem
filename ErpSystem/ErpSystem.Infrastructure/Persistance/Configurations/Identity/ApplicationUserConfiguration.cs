using ErpSystem.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Identity;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);

        builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);

        builder.Property(u => u.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(u => u.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
    }
}
