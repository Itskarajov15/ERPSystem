using System.Diagnostics.CodeAnalysis;
using ErpSystem.Domain.Entities.Identity;

namespace ErpSystem.Infrastructure.Comparers;

public class RoutePermissionComparer : IEqualityComparer<RoutePermission>
{
    public bool Equals(RoutePermission? x, RoutePermission? y)
    {
        if (x != null && y != null)
        {
            return x.ControllerName == y.ControllerName && x.ActionName == y.ActionName;
        }

        return false;
    }

    public int GetHashCode([DisallowNull] RoutePermission obj)
    {
        return HashCode.Combine(obj.ControllerName, obj.ActionName);
    }
}
