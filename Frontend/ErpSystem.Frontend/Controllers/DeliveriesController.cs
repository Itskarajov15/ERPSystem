using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Deliveries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Frontend.Controllers;

[Authorize]
public class DeliveriesController : Controller
{
    private readonly IDeliveryService _deliveryService;
    private readonly ISupplierService _supplierService;
    private readonly IProductService _productService;

    public DeliveriesController(
        IDeliveryService deliveryService,
        ISupplierService supplierService,
        IProductService productService
    )
    {
        _deliveryService = deliveryService;
        _supplierService = supplierService;
        _productService = productService;
    }

    public async Task<IActionResult> Index([FromQuery] DeliveryFilterModel filter)
    {
        try
        {
            var deliveries = await _deliveryService.GetDeliveriesAsync(filter);
            await LoadSuppliersDropdown();
            return View(deliveries);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            await LoadSuppliersDropdown();
            return View(new PageResult<DeliveryViewModel>());
        }
    }

    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var delivery = await _deliveryService.GetDeliveryByIdAsync(id);
            if (delivery == null)
            {
                TempData["ErrorMessage"] = "Доставката не е намерена";
                return RedirectToAction(nameof(Index));
            }

            return View(delivery);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            await LoadViewBagData();
            return View(new DeliveryCreateModel());
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DeliveryCreateModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadViewBagData();
            return View(model);
        }

        try
        {
            var deliveryId = await _deliveryService.CreateDeliveryAsync(model);
            TempData["SuccessMessage"] = "Доставката е създадена успешно";
            return RedirectToAction(nameof(Details), new { id = deliveryId });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            await LoadViewBagData();
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartProgress(Guid id)
    {
        try
        {
            await _deliveryService.StartDeliveryProgressAsync(id);
            TempData["SuccessMessage"] = "Доставката е започната успешно";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete(Guid id)
    {
        try
        {
            await _deliveryService.CompleteDeliveryAsync(id);
            TempData["SuccessMessage"] = "Доставката е завършена успешно";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(Guid id)
    {
        try
        {
            await _deliveryService.DeleteDeliveryAsync(id);
            TempData["SuccessMessage"] = "Доставката е отменена успешно";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deliveryService.DeleteDeliveryAsync(id);
            TempData["SuccessMessage"] = "Доставката е изтрита успешно";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            var products = await _productService.GetProductsAsync();
            var productList = products
                .Items.Select(p => new
                {
                    id = p.Id,
                    name = p.Name,
                    sku = p.Sku,
                    price = p.UnitPrice,
                    quantity = p.Quantity,
                })
                .ToList();

            return Json(productList);
        }
        catch (Exception ex)
        {
            return Json(new { error = ex.Message });
        }
    }

    private async Task LoadViewBagData()
    {
        var suppliers = await _supplierService.GetSuppliersAsync();
        ViewBag.Suppliers = suppliers
            .Items.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
            .ToList();
    }

    private async Task LoadSuppliersDropdown()
    {
        var suppliers = await _supplierService.GetSuppliersAsync();
        ViewBag.Suppliers = suppliers
            .Items.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
            .ToList();
    }
}
