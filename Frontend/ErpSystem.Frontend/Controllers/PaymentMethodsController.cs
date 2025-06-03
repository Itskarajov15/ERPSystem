using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.PaymentMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.Frontend.Controllers;

[Authorize]
public class PaymentMethodsController : Controller
{
    private readonly IPaymentMethodService _paymentMethodService;

    public PaymentMethodsController(IPaymentMethodService paymentMethodService)
    {
        _paymentMethodService = paymentMethodService;
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
            TempData["ErrorMessage"] = ex.Message;
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
            TempData["ErrorMessage"] = ex.Message;
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
                Name = paymentMethod.Name
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
            TempData["ErrorMessage"] = ex.Message;
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
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
} 