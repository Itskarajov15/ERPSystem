using Microsoft.AspNetCore.Identity;

namespace ErpSystem.Infrastructure.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        this.Id = Guid.NewGuid();
    }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool IsDeleted { get; set; }
}
