﻿using ErpSystem.Domain.Abstractions;

namespace ErpSystem.Domain.Entities.Identity;

public class RoutePermission : BaseEntity
{
    public string ActionName { get; set; } = null!;

    public string ControllerName { get; set; } = null!;

    public ICollection<RoleRoutePermission> RoleRoutePermissions { get; set; } =
        new HashSet<RoleRoutePermission>();
}
