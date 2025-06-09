using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.PaymentMethods;
using ErpSystem.Frontend.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.Frontend.Controllers;

[Authorize]
public class PaymentMethodsController : Controller
{
    private readonly IPaymentMethodService _paymentMethodService;
    private readonly ErrorTranslationService _errorTranslationService;

    public PaymentMethodsController(
        IPaymentMethodService paymentMethodService,
        ErrorTranslationService errorTranslationService
    )
    {
        _paymentMethodService = paymentMethodService;
        _errorTranslationService = errorTranslationService;
    }

    public async Task<IActionResult> Index([FromQuery] PaymentMethodFilterModel filter)
    {
        try
        {
            var paymentMethods = await _paymentMethodService.GetPaymentMethodsAsync(filter);
            return View(paymentMethods);
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            TempData["ErrorMessage"] = translatedMessage;
            return View(new Core.Models.Common.PageResult<PaymentMethodViewModel>());
        }
    }

    public IActionResult Create()
    {
        return View(new PaymentMethodEditModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PaymentMethodEditModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _paymentMethodService.AddPaymentMethodAsync(model);
                TempData["SuccessMessage"] = "Начинът на плащане беше създаден успешно.";
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
            var paymentMethod = await _paymentMethodService.GetPaymentMethodByIdAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            var model = new PaymentMethodEditModel
            {
                Id = paymentMethod.Id,
                Name = paymentMethod.Name,
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
    public async Task<IActionResult> Edit(PaymentMethodEditModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _paymentMethodService.UpdatePaymentMethodAsync(model);
                TempData["SuccessMessage"] = "Начинът на плащане беше обновен успешно.";
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
            await _paymentMethodService.DeletePaymentMethodAsync(id);
            TempData["SuccessMessage"] = "Начинът на плащане беше изтрит успешно.";
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            TempData["ErrorMessage"] = translatedMessage;
        }

        return RedirectToAction(nameof(Index));
    }
}
