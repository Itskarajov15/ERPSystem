using ErpSystem.Application.Products.Commands.AddProduct;
using ErpSystem.Application.Products.Commands.DeleteProduct;
using ErpSystem.Application.Products.Commands.UpdateProduct;
using ErpSystem.Application.Products.Queries.GetProductById;
using ErpSystem.Application.Products.Queries.GetProducts;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

[Route("api/products")]
public class ProductsController : BaseController
{
    public ProductsController(IMediator mediator)
        : base(mediator) { }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] ProductFilters? filters = null
    )
    {
        var query = new GetProductsQuery(paginationParams, filters);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(AddProductCommand command)
    {
        var productId = await _mediator.Send(command);

        return Ok(productId);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
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
        await _mediator.Send(new DeleteProductCommand(id));

        return NoContent();
    }
}
