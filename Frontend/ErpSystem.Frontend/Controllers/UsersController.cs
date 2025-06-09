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

    public async Task<IActionResult> Index(int page = 1, int pageSize = 25, string searchTerm = "")
    {
        ViewBag.CurrentPage = page;
        var users = await _userService.GetUsersAsync(page, pageSize, searchTerm);
        return View(users);
    }

    public async Task<IActionResult> Create()
    {
        var roles = await _userService.GetRolesAsync();
        var model = new CreateUserViewModel { AvailableRoles = roles.Items.ToList() };
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
            TempData["SuccessMessage"] = "Потребителят е създаден успешно.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(
            string.Empty,
            "Неуспешно създаване на потребител. Моля опитайте отново."
        );
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
            return BadRequest("Името на ролята е задължително.");
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

        return BadRequest("Неуспешно създаване на роля.");
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _userService.GetRolesAsync();
        return Json(roles);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.RoleName))
        {
            return BadRequest("Името на ролята е задължително.");
        }

        var result = await _userService.AssignRoleAsync(request.UserId, request.RoleName);
        if (result)
        {
            return Ok();
        }

        return BadRequest("Неуспешно присвояване на роля.");
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

        return BadRequest("Неуспешно премахване на роля.");
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

        return BadRequest("Неуспешно изтриване на роля.");
    }
}
