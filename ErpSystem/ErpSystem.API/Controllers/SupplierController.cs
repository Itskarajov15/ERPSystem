using ErpSystem.Application.Suppliers.Commands.AddSupplier;
using ErpSystem.Application.Suppliers.Commands.DeleteSupplier;
using ErpSystem.Application.Suppliers.Commands.UpdateSupplier;
using ErpSystem.Application.Suppliers.Queries.GetSupplierById;
using ErpSystem.Application.Suppliers.Queries.GetSuppliers;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

public class SupplierController : BaseController
{
    public SupplierController(IMediator mediator)
        : base(mediator) { }

    [HttpGet]
    public async Task<IActionResult> GetSuppliers(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] SupplierFilters? filters = null
    )
    {
        var query = new GetSuppliersQuery(paginationParams, filters);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSupplierById(Guid id)
    {
        var query = new GetSupplierByIdQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddSupplier(AddSupplierCommand command)
    {
        var supplierId = await _mediator.Send(command);

        return Ok(supplierId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSupplier(
        Guid id,
        [FromBody] UpdateSupplierCommand command
    )
    {
        if (id != command.Id)
        {
            return BadRequest("ID in the URL does not match the ID in the request body");
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupplier(Guid id)
    {
        await _mediator.Send(new DeleteSupplierCommand(id));

        return NoContent();
    }
}
