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

public class OrderController : BaseController
{
    public OrderController(IMediator mediator)
        : base(mediator) { }

    [HttpGet]
    public async Task<IActionResult> GetOrders(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] OrderFilters? filters = null
    )
    {
        var query = new GetOrdersQuery(filters, paginationParams);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDetails(Guid id)
    {
        var query = new GetOrderDetailsQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder(AddOrderCommand command)
    {
        var orderId = await _mediator.Send(command);

        return Ok(orderId);
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteOrder(Guid id)
    {
        await _mediator.Send(new CompleteOrderCommand(id));

        return NoContent();
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        await _mediator.Send(new CancelOrderCommand(id));

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        await _mediator.Send(new DeleteOrderCommand(id));

        return NoContent();
    }
}
