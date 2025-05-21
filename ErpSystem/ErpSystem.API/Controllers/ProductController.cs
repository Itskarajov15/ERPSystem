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

public class ProductController : BaseController
{
    public ProductController(IMediator mediator)
        : base(mediator) { }

    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] ProductFilters? filters = null
    )
    {
        var query = new GetProductsQuery(paginationParams, filters);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(AddProductCommand command)
    {
        var productId = await _mediator.Send(command);

        return Ok(productId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID in the URL does not match the ID in the request body");
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));

        return NoContent();
    }
}
