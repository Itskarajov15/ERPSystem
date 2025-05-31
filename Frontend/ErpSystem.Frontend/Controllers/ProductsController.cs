using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Frontend.Controllers;

[Authorize]
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IUnitOfMeasureService _unitOfMeasureService;

    public ProductsController(
        IProductService productService,
        IUnitOfMeasureService unitOfMeasureService
    )
    {
        _productService = productService;
        _unitOfMeasureService = unitOfMeasureService;
    }

    public async Task<IActionResult> Index([FromQuery] ProductFilterModel filter)
    {
        try
        {
            var products = await _productService.GetProductsAsync(filter);
            return View(products);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View(new Core.Models.Common.PageResult<ProductViewModel>());
        }
    }

    public async Task<IActionResult> Create()
    {
        try
        {
            await PopulateUnitsOfMeasureDropdown();
            return View(new ProductEditModel());
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductEditModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(model);
                TempData["SuccessMessage"] = "Продуктът беше създаден успешно.";
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        await PopulateUnitsOfMeasureDropdown();
        return View(model);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductEditModel
            {
                Id = product.Id,
                Name = product.Name,
                Sku = product.Sku,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                ReorderLevel = product.ReorderLevel,
                UnitOfMeasureId = product.UnitOfMeasureId,
            };

            await PopulateUnitsOfMeasureDropdown();
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
    public async Task<IActionResult> Edit(ProductEditModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(model);
                TempData["SuccessMessage"] = "Продуктът беше обновен успешно.";
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        await PopulateUnitsOfMeasureDropdown();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
            TempData["SuccessMessage"] = "Продуктът беше изтрит успешно.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateUnitsOfMeasureDropdown()
    {
        var units = await _unitOfMeasureService.GetUnitsOfMeasureAsync();
        ViewBag.UnitsOfMeasure = units
            .Items.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name })
            .ToList();
    }
}
