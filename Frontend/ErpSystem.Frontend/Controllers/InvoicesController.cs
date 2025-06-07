using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Invoices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.Frontend.Controllers;

[Authorize]
public class InvoicesController : Controller
{
    private readonly IInvoiceService _invoiceService;
    private readonly IPaymentMethodService _paymentMethodService;

    public InvoicesController(
        IInvoiceService invoiceService,
        IPaymentMethodService paymentMethodService
    )
    {
        _invoiceService = invoiceService;
        _paymentMethodService = paymentMethodService;
    }

    public async Task<IActionResult> Index([FromQuery] InvoiceFilterModel filter)
    {
        try
        {
            var invoices = await _invoiceService.GetInvoicesAsync(filter);
            return View(invoices);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View(new Core.Models.Common.PageResult<InvoiceViewModel>());
        }
    }

    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            if (invoice.CanRecordPayment)
            {
                ViewBag.PaymentMethods = await _paymentMethodService.GetAllPaymentMethodsAsync();
            }

            return View(invoice);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateFromOrder(Guid orderId, string? notes = null)
    {
        try
        {
            var invoiceId = await _invoiceService.CreateInvoiceFromOrderAsync(orderId, notes);
            TempData["SuccessMessage"] = "Фактурата беше създадена успешно.";
            TempData["OpenPdfInNewTab"] = Url.Action("GetPdf", "Invoices", new { id = invoiceId });
            return RedirectToAction(nameof(Details), new { id = invoiceId });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Details", "Orders", new { id = orderId });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(Guid id, InvoiceStatus status)
    {
        try
        {
            await _invoiceService.UpdateInvoiceStatusAsync(id, status);
            TempData["SuccessMessage"] = "Статусът на фактурата беше обновен успешно.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { id });
    }

    public async Task<IActionResult> GetPdf(Guid id)
    {
        try
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);

            if (invoice == null)
                return NotFound();

            var pdfBytes = await _invoiceService.GetInvoicePdfAsync(id);

            return File(pdfBytes, "application/pdf", $"Invoice_{invoice.InvoiceNumber}.pdf");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
