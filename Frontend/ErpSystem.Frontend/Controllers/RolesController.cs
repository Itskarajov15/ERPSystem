using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Roles;
using ErpSystem.Frontend.Core.Models.Users;
using ErpSystem.Frontend.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.Frontend.Controllers;

[Authorize(Roles = "Admin")]
public class RolesController : Controller
{
    private readonly IRoleService _roleService;
    private readonly ErrorTranslationService _errorTranslationService;

    public RolesController(
        IRoleService roleService,
        ErrorTranslationService errorTranslationService
    )
    {
        _roleService = roleService;
        _errorTranslationService = errorTranslationService;
    }

    public async Task<IActionResult> Index([FromQuery] RoleFilterModel filter)
    {
        try
        {
            var roles = await _roleService.GetRolesAsync(filter);
            return View(roles);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View(new PageResult<RoleViewModel>());
        }
    }

    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    public async Task<IActionResult> Create()
    {
        try
        {
            var availableEndpoints = await _roleService.GetAvailableEndpointsAsync();
            var model = new RoleEditModel { AvailableEndpoints = availableEndpoints };
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoleEditModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _roleService.AddRoleAsync(model);
                TempData["SuccessMessage"] = "Ролята беше създадена успешно.";
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        model.AvailableEndpoints = await _roleService.GetAvailableEndpointsAsync();
        return View(model);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var availableEndpoints = await _roleService.GetAvailableEndpointsAsync();
            var model = new RoleEditModel
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                SelectedPermissionIds =
                    role.Permissions?.Select(p => p.Id.ToString()).ToList() ?? new List<string>(),
                AvailableEndpoints = availableEndpoints,
            };

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(RoleEditModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _roleService.UpdateRoleAsync(model);
                TempData["SuccessMessage"] = "Ролята беше обновена успешно.";
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        model.AvailableEndpoints = await _roleService.GetAvailableEndpointsAsync();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _roleService.DeleteRoleAsync(id);
            TempData["SuccessMessage"] = "Ролята е изтрита успешно";
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            TempData["ErrorMessage"] = translatedMessage;
        }

        return RedirectToAction(nameof(Index));
    }
}
