using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Frontend.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ICustomerService _customerService;
    private readonly IProductService _productService;
    private readonly IPaymentMethodService _paymentMethodService;

    public OrdersController(
        IOrderService orderService,
        ICustomerService customerService,
        IProductService productService,
        IPaymentMethodService paymentMethodService
    )
    {
        _orderService = orderService;
        _customerService = customerService;
        _productService = productService;
        _paymentMethodService = paymentMethodService;
    }

    public async Task<IActionResult> Index([FromQuery] OrderFilterModel filter)
    {
        try
        {
            var orders = await _orderService.GetOrdersAsync(filter);
            await PopulateCustomersDropdown();
            return View(orders);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View(new Core.Models.Common.PageResult<OrderViewModel>());
        }
    }

    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    public async Task<IActionResult> Create()
    {
        try
        {
            await PopulateDropdowns();
            return View(new OrderCreateModel());
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderCreateModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _orderService.AddOrderAsync(model);
                TempData["SuccessMessage"] = "Поръчката беше създадена успешно.";
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        await PopulateDropdowns();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete(Guid id)
    {
        try
        {
            await _orderService.CompleteOrderAsync(id);
            TempData["SuccessMessage"] = "Поръчката беше завършена успешно.";
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
            await _orderService.CancelOrderAsync(id);
            TempData["SuccessMessage"] = "Поръчката беше отменена успешно.";
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
            await _orderService.DeleteOrderAsync(id);
            TempData["SuccessMessage"] = "Поръчката беше изтрита успешно.";
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

    private async Task PopulateDropdowns()
    {
        await PopulateCustomersDropdown();
        await PopulatePaymentMethodsDropdown();
    }

    private async Task PopulateCustomersDropdown()
    {
        var customers = await _customerService.GetCustomersAsync();
        ViewBag.Customers = customers
            .Items.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            .ToList();
    }

    private async Task PopulatePaymentMethodsDropdown()
    {
        var paymentMethods = await _paymentMethodService.GetPaymentMethodsAsync();
        ViewBag.PaymentMethods = paymentMethods
            .Items.Select(pm => new SelectListItem { Value = pm.Id.ToString(), Text = pm.Name })
            .ToList();
    }
}
