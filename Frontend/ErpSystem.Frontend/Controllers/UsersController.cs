using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.Frontend.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        ViewBag.CurrentPage = page;
        var users = await _userService.GetUsersAsync(page);
        return View(users);
    }

    public async Task<IActionResult> Create()
    {
        var roles = await _userService.GetRolesAsync();
        var model = new CreateUserViewModel
        {
            AvailableRoles = roles.Items.ToList()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var roles = await _userService.GetRolesAsync();
            model.AvailableRoles = roles.Items.ToList();
            return View(model);
        }

        var result = await _userService.RegisterUserAsync(model);
        if (result)
        {
            TempData["SuccessMessage"] = "User created successfully.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "Failed to create user. Please try again.");
        var availableRoles = await _userService.GetRolesAsync();
        model.AvailableRoles = availableRoles.Items.ToList();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> GetAvailableEndpoints()
    {
        var endpoints = await _userService.GetAvailableEndpointsAsync();
        return Json(endpoints);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Role name is required.");
        }

        var result = await _userService.CreateRoleAsync(
            request.Name,
            request.Description,
            request.PermissionIds
        );

        if (result)
        {
            return Ok();
        }

        return BadRequest("Failed to create role.");
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _userService.GetRolesAsync();
        return Json(roles);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserRoles(string userId)
    {
        var roles = await _userService.GetRolesAsync();
        var users = await _userService.GetUsersAsync();
        var user = users.Items.FirstOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return NotFound();
        }

        return Json(new { availableRoles = roles.Items, userRoles = user.Roles.Select(r => r.Name) });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.RoleName))
        {
            return BadRequest("Role name is required.");
        }

        var result = await _userService.AssignRoleAsync(request.UserId, request.RoleName);
        if (result)
        {
            return Ok();
        }

        return BadRequest("Failed to assign role.");
    }

    [HttpDelete]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveRole(string userId, string roleName)
    {
        var result = await _userService.RemoveRoleAsync(userId, roleName);
        if (result)
        {
            return Ok();
        }

        return BadRequest("Failed to remove role.");
    }

    [HttpDelete]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteRole(string roleId)
    {
        var result = await _userService.DeleteRoleAsync(roleId);
        if (result)
        {
            return Ok();
        }

        return BadRequest("Failed to delete role.");
    }
}

public class CreateRoleRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> PermissionIds { get; set; } = new();
}

public class AssignRoleRequest
{
    public string UserId { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}
