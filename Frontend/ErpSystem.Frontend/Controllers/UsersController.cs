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
        var model = new CreateUserViewModel
        {
            AvailableRoles = await _userService.GetAvailableRolesAsync(),
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.AvailableRoles = await _userService.GetAvailableRolesAsync();
            return View(model);
        }

        var result = await _userService.CreateUserAsync(model);
        if (result)
        {
            TempData["SuccessMessage"] = "User created successfully.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "Failed to create user. Please try again.");
        model.AvailableRoles = await _userService.GetAvailableRolesAsync();
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
    public async Task<IActionResult> GetUserRoles(string userId)
    {
        var roles = await _userService.GetAvailableRolesAsync();
        var users = await _userService.GetUsersAsync();
        var user = users.Items.FirstOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return NotFound();
        }

        return Json(new { availableRoles = roles, userRoles = user.Roles.Select(r => r.Name) });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesRequest request)
    {
        if (request.Roles == null)
        {
            return BadRequest("Roles are required.");
        }

        var users = await _userService.GetUsersAsync();
        var currentUser = users.Items.FirstOrDefault(u => u.Id == request.UserId);

        if (currentUser == null)
        {
            return NotFound();
        }

        var success = true;

        foreach (var role in currentUser.Roles)
        {
            if (!request.Roles.Contains(role.Name))
            {
                success &= await _userService.RemoveRoleAsync(request.UserId, role.Name);
            }
        }

        foreach (var role in request.Roles)
        {
            if (!currentUser.Roles.Any(r => r.Name == role))
            {
                success &= await _userService.AssignRoleAsync(request.UserId, role);
            }
        }

        if (success)
        {
            return Ok();
        }

        return BadRequest("Failed to update user roles.");
    }

    [HttpDelete]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok();
    }
}

public class CreateRoleRequest
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<string> PermissionIds { get; set; } = new();
}

public class UpdateUserRolesRequest
{
    public string UserId { get; set; } = string.Empty;

    public List<string> Roles { get; set; } = new();
}
