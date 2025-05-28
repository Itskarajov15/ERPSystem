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

[Route("api/suppliers")]
public class SuppliersController : BaseController
{
    public SuppliersController(IMediator mediator)
        : base(mediator) { }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] SupplierFilters? filters = null
    )
    {
        var query = new GetSuppliersQuery(paginationParams, filters);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetSupplierByIdQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(AddSupplierCommand command)
    {
        var supplierId = await _mediator.Send(command);

        return Ok(supplierId);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSupplierCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID in the URL does not match the ID in the request body");
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteSupplierCommand(id));

        return NoContent();
    }
}
