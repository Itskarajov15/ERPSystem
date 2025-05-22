using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ErpSystem.Application.Authentication.DTOs;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Application.Common.Interfaces;
using ErpSystem.Application.Common.Models;
using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ErpSystem.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IPermissionService _permissionService;
    private readonly IRepository _repository;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IConfiguration configuration,
        IPermissionService permissionService,
        IRepository repository
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _permissionService = permissionService;
        _repository = repository;
    }

    public async Task AddToRoleAsync(Guid userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            throw new NotFoundException("User", userId);
        }

        if (!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new ApplicationRole { Name = role });
        }

        var result = await _userManager.AddToRoleAsync(user, role);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to add user to role");
        }
    }

    public async Task<AuthenticationResult> AuthenticateAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null || !user.IsActive)
        {
            throw new NotFoundException("User", userName);
        }

        var result = await _userManager.CheckPasswordAsync(user, password);

        if (!result)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var token = await GenerateJwtTokenAsync(user);

        user.LastLogin = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        return new AuthenticationResult()
        {
            UserId = user.Id.ToString(),
            UserName = user.UserName,
            AccessToken = token,
        };
    }

    public async Task<RoleDto> CreateRoleAsync(string name, string description)
    {
        if (await _roleManager.RoleExistsAsync(name))
        {
            throw new Exception("Role already exists");
        }

        var role = new ApplicationRole { Name = name, Description = description };

        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            throw new Exception("Role creation failed");
        }

        return new RoleDto
        {
            Id = role.Id.ToString(),
            Name = role.Name,
            Description = role.Description,
        };
    }

    public async Task<string> CreateUserAsync(
        string userName,
        string password,
        string email,
        string firstName,
        string lastName
    )
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = email,
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            FirstName = firstName,
            LastName = lastName,
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            throw new Exception("User creation failed");
        }

        return user.Id.ToString();
    }

    public async Task DeleteRoleAsync(Guid id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role == null)
        {
            throw new NotFoundException("Role", id);
        }

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to delete role");
        }
    }

    public async Task<bool> CheckRoleAccessAsync(string roleName, string action, string controller)
    {
        var role = await _roleManager.FindByNameAsync(roleName);

        if (role == null)
        {
            throw new ArgumentException("Role with this name does not exist.");
        }

        return _repository
            .All<RoleRoutePermission>()
            .Any(rrp =>
                rrp.RoleId == role.Id
                && rrp.RoutePermission.ActionName == action
                && rrp.RoutePermission.ControllerName == controller
            );
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            return;
        }

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to delete user");
        }
    }

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();

        return roles.Select(role => new RoleDto
        {
            Id = role.Id.ToString(),
            Name = role.Name!,
            Description = role.Description,
        });
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        return user.UserName;
    }

    public async Task<bool> IsInRoleAsync(Guid userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            throw new NotFoundException("User", userId);
        }

        return await _userManager.IsInRoleAsync(user, role);
    }

    public async Task RemoveRoleAsync(Guid userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            throw new NotFoundException("User", userId);
        }

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to remove role from user");
        }
    }

    private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id.ToString()),
        };

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var permissionClaims = await _permissionService.GetPermissionClaimsAsync(user);
        claims.AddRange(permissionClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddHours(Convert.ToDouble(_configuration["Jwt:ExpiryInHours"]));

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
