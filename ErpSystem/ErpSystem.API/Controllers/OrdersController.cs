using ErpSystem.Application.Orders.Commands.AddOrder;
using ErpSystem.Application.Orders.Commands.CancelOrder;
using ErpSystem.Application.Orders.Commands.CompleteOrder;
using ErpSystem.Application.Orders.Commands.DeleteOrder;
using ErpSystem.Application.Orders.Queries.GetOrderDetails;
using ErpSystem.Application.Orders.Queries.GetOrders;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

[Route("api/orders")]
public class OrdersController : BaseController
{
    public OrdersController(IMediator mediator)
        : base(mediator) { }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] OrderFilters? filters = null
    )
    {
        var query = new GetOrdersQuery(filters, paginationParams);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetOrderDetailsQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> ADd(AddOrderCommand command)
    {
        var orderId = await _mediator.Send(command);

        return Ok(orderId);
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        await _mediator.Send(new CompleteOrderCommand(id));

        return NoContent();
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        await _mediator.Send(new CancelOrderCommand(id));

        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteOrderCommand(id));

        return NoContent();
    }
}
