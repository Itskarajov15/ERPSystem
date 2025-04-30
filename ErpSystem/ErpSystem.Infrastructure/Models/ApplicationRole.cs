using Microsoft.AspNetCore.Identity;

namespace ErpSystem.Infrastructure.Models;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole()
    {
        this.Id = Guid.NewGuid();
    }

    public string Description { get; set; } = null!;
}
