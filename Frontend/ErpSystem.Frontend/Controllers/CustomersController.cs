using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.Frontend.Controllers;

[Authorize]
public class CustomersController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<IActionResult> Index([FromQuery] CustomerFilterModel filter)
    {
        try
        {
            var customers = await _customerService.GetCustomersAsync(filter);
            return View(customers);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View(new PageResult<CustomerViewModel>());
        }
    }

    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    public IActionResult Create()
    {
        return View(new CustomerEditModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustomerEditModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _customerService.AddCustomerAsync(model);
                TempData["SuccessMessage"] = "Клиентът беше създаден успешно.";
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
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            var model = new CustomerEditModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                ContactName = customer.ContactName,
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
    public async Task<IActionResult> Edit(CustomerEditModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _customerService.UpdateCustomerAsync(model);
                TempData["SuccessMessage"] = "Клиентът беше обновен успешно.";
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return View(model);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _customerService.DeleteCustomerAsync(id);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }
}
