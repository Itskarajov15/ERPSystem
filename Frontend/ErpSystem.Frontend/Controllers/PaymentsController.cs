using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Payments;
using ErpSystem.Frontend.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.Frontend.Controllers;

[Authorize]
public class PaymentsController : Controller
{
    private readonly IPaymentService _paymentService;
    private readonly ICustomerService _customerService;
    private readonly ErrorTranslationService _errorTranslationService;

    public PaymentsController(
        IPaymentService paymentService,
        ICustomerService customerService,
        ErrorTranslationService errorTranslationService
    )
    {
        _paymentService = paymentService;
        _customerService = customerService;
        _errorTranslationService = errorTranslationService;
    }

    public async Task<IActionResult> Index([FromQuery] PaymentFilterModel filter)
    {
        try
        {
            var payments = await _paymentService.GetPaymentsAsync(filter);
            var customers = await _customerService.GetCustomersAsync();
            ViewBag.Customers = customers.Items;
            return View(payments);
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            TempData["ErrorMessage"] = translatedMessage;
            return View(new PageResult<PaymentViewModel>());
        }
    }

    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
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
    public async Task<IActionResult> RecordPayment(RecordPaymentRequest request)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Моля, попълнете всички задължителни полета правилно.";
            return RedirectToAction("Details", "Invoices", new { id = request.InvoiceId });
        }

        try
        {
            await _paymentService.RecordPaymentAsync(request);
            TempData["SuccessMessage"] = "Плащането беше записано успешно.";
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            TempData["ErrorMessage"] = translatedMessage;
        }

        return RedirectToAction("Details", "Invoices", new { id = request.InvoiceId });
    }
}
