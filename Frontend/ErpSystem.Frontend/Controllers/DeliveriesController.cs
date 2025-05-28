using ErpSystem.Frontend.Core.Interfaces;
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
        var viewModel = new DeliveriesViewModel { Filter = filter };

        try
        {
            var suppliers = await _supplierService.GetSuppliersAsync();
            viewModel.Suppliers = suppliers
                .Items.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToList();

            var products = await _productService.GetProductsAsync();
            viewModel.Products = products
                .Items.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.Name} ({p.Sku})",
                })
                .ToList();

            var (items, totalCount) = await _deliveryService.GetDeliveriesAsync(filter);
            viewModel.Deliveries = items;
            viewModel.TotalCount = totalCount;
        }
        catch (Exception ex)
        {
            viewModel.ErrorMessage = ex.Message;
        }

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetDelivery(Guid id)
    {
        try
        {
            var delivery = await _deliveryService.GetDeliveryByIdAsync(id);
            if (delivery == null)
                return NotFound(new { message = "Delivery not found" });

            return Json(delivery);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DeliveryEditModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    new { message = "Please check the form and try again", errors = ModelState }
                );

            var id = await _deliveryService.CreateDeliveryAsync(model);
            return Json(new { id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Complete(Guid id)
    {
        try
        {
            await _deliveryService.CompleteDeliveryAsync(id);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deliveryService.DeleteDeliveryAsync(id);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int id, string status)
    {
        var result = await _deliveryService.UpdateDeliveryStatusAsync(id, status);
        return Json(new { success = result });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateComment(int id, string comment)
    {
        var result = await _deliveryService.UpdateDeliveryCommentAsync(id, comment);
        return Json(new { success = result });
    }

    [HttpGet]
    public async Task<IActionResult> Export([FromQuery] DeliveryFilterModel filter)
    {
        await _deliveryService.ExportDeliveriesAsync(filter);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> ImportPriorities(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");

        await _deliveryService.ImportPrioritiesAsync(file);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> StartProgress(Guid id)
    {
        try
        {
            await _deliveryService.StartDeliveryProgressAsync(id);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
