using ErpSystem.Application.Customers.Commands.AddCustomer;
using ErpSystem.Application.Customers.Commands.DeleteCustomer;
using ErpSystem.Application.Customers.Commands.UpdateCustomer;
using ErpSystem.Application.Customers.Queries.GetCustomerById;
using ErpSystem.Application.Customers.Queries.GetCustomers;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

[Route("api/customers")]
public class CustomersController : BaseController
{
    public CustomersController(IMediator mediator)
        : base(mediator) { }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] CustomerFilters? customerFilters = null
    )
    {
        var query = new GetCustomersQuery(paginationParams, customerFilters);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetCustomerByIdQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(AddCustomerCommand command)
    {
        var customerId = await _mediator.Send(command);

        return Ok(customerId);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateCustomerCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Customer ID mismatch.");
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteCustomerCommand(id));

        return NoContent();
    }
}
