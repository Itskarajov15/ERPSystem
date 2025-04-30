namespace ErpSystem.Infrastructure.Models;

public class Permission : BaseClass
{
    public string Name { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public string ActionType { get; set; } = null!;

    public string Description { get; set; } = null!;

    public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
}
