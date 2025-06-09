using System.ComponentModel.DataAnnotations;
using ErpSystem.Frontend.Core.Models.Users;

namespace ErpSystem.Frontend.Core.Models.Roles;

public class RoleEditModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Името на ролята е задължително")]
    [StringLength(100, ErrorMessage = "Името на ролята не може да бъде по-дълго от 100 символа")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Описанието не може да бъде по-дълго от 500 символа")]
    public string? Description { get; set; }

    public List<string> SelectedPermissionIds { get; set; } = new();

    public List<EndpointViewModel> AvailableEndpoints { get; set; } = new();
}
