using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Suppliers;
using ErpSystem.Frontend.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.Frontend.Controllers;

[Authorize]
public class SuppliersController : Controller
{
    private readonly ISupplierService _supplierService;
    private readonly ErrorTranslationService _errorTranslationService;

    public SuppliersController(
        ISupplierService supplierService,
        ErrorTranslationService errorTranslationService
    )
    {
        _supplierService = supplierService;
        _errorTranslationService = errorTranslationService;
    }

    public async Task<IActionResult> Index([FromQuery] SupplierFilterModel filter)
    {
        try
        {
            var suppliers = await _supplierService.GetSuppliersAsync(filter);
            return View(suppliers);
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            TempData["ErrorMessage"] = translatedMessage;
            return View(new PageResult<SupplierViewModel>());
        }
    }

    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            TempData["ErrorMessage"] = translatedMessage;
            return RedirectToAction(nameof(Index));
        }
    }

    public IActionResult Create()
    {
        return View(new SupplierEditModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SupplierEditModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _supplierService.AddSupplierAsync(model);
                TempData["SuccessMessage"] = "Доставчикът беше създаден успешно.";
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            TempData["ErrorMessage"] = translatedMessage;
        }

        return View(model);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            var model = new SupplierEditModel
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Email = supplier.Email,
                Phone = supplier.PhoneNumber,
                Address = supplier.Address,
                ContactPerson = supplier.ContactName,
            };

            return View(model);
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            TempData["ErrorMessage"] = translatedMessage;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SupplierEditModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _supplierService.UpdateSupplierAsync(model);
                TempData["SuccessMessage"] = "Доставчикът беше обновен успешно.";
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            TempData["ErrorMessage"] = translatedMessage;
        }

        return View(model);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _supplierService.DeleteSupplierAsync(id);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            return Json(new { success = false, message = translatedMessage });
        }
    }
}
