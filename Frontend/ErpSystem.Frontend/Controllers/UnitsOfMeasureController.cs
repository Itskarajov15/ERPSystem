using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.UnitsOfMeasure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.Frontend.Controllers;

[Authorize]
public class UnitsOfMeasureController : Controller
{
    private readonly IUnitOfMeasureService _unitOfMeasureService;

    public UnitsOfMeasureController(IUnitOfMeasureService unitOfMeasureService)
    {
        _unitOfMeasureService = unitOfMeasureService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        try
        {
            ViewBag.CurrentPage = page;
            var unitsOfMeasure = await _unitOfMeasureService.GetUnitsOfMeasureAsync(page);
            return View(unitsOfMeasure);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View(null);
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new UnitOfMeasureEditModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UnitOfMeasureEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _unitOfMeasureService.CreateUnitOfMeasureAsync(model);
            TempData["SuccessMessage"] = "Мерната единица е създадена успешно.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var unit = await _unitOfMeasureService.GetUnitOfMeasureByIdAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            var model = new UnitOfMeasureEditModel { Id = unit.Id, Name = unit.Name };

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
    public async Task<IActionResult> Edit(UnitOfMeasureEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _unitOfMeasureService.UpdateUnitOfMeasureAsync(model);
            TempData["SuccessMessage"] = "Мерната единица е обновена успешно.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _unitOfMeasureService.DeleteUnitOfMeasureAsync(id);
            TempData["SuccessMessage"] = "Мерната единица е изтрита успешно.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
