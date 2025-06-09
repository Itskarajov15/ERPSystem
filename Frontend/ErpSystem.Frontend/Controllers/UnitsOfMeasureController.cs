using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.UnitsOfMeasure;
using ErpSystem.Frontend.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.Frontend.Controllers;

[Authorize]
public class UnitsOfMeasureController : Controller
{
    private readonly IUnitOfMeasureService _unitOfMeasureService;
    private readonly ErrorTranslationService _errorTranslationService;

    public UnitsOfMeasureController(
        IUnitOfMeasureService unitOfMeasureService,
        ErrorTranslationService errorTranslationService
    )
    {
        _unitOfMeasureService = unitOfMeasureService;
        _errorTranslationService = errorTranslationService;
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
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            TempData["ErrorMessage"] = translatedMessage;
            return View(new PageResult<UnitOfMeasureViewModel>());
        }
    }

    public IActionResult Create()
    {
        return View(new UnitOfMeasureEditModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UnitOfMeasureEditModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _unitOfMeasureService.CreateUnitOfMeasureAsync(model);
                TempData["SuccessMessage"] = "Мерната единица беше създадена успешно.";
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
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            TempData["ErrorMessage"] = translatedMessage;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UnitOfMeasureEditModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _unitOfMeasureService.UpdateUnitOfMeasureAsync(model);
                TempData["SuccessMessage"] = "Мерната единица беше обновена успешно.";
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _unitOfMeasureService.DeleteUnitOfMeasureAsync(id);
            TempData["SuccessMessage"] = "Мерната единица беше изтрита успешно.";
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            TempData["ErrorMessage"] = translatedMessage;
        }

        return RedirectToAction(nameof(Index));
    }
}
