using System.Security.Claims;
using ErpSystem.Domain.Entities.Identity;

namespace ErpSystem.Application.Common.Interfaces;

public interface IPermissionService
{
    Task<List<Claim>> GetPermissionClaimsAsync(ApplicationUser user);
}
