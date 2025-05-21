using ErpSystem.Application.Deliveries.Commands.AddDelivery;
using ErpSystem.Application.Deliveries.Commands.CompleteDelivery;
using ErpSystem.Application.Deliveries.Commands.DeleteDelivery;
using ErpSystem.Application.Deliveries.Queries.GetDeliveries;
using ErpSystem.Application.Deliveries.Queries.GetDeliveryDetails;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

public class DeliveryController : BaseController
{
    public DeliveryController(IMediator mediator)
        : base(mediator) { }

    [HttpGet]
    public async Task<IActionResult> GetDeliveries(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] DeliveryFilters? filters = null
    )
    {
        var query = new GetDeliveriesQuery(paginationParams, filters);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeliveryDetails(Guid id)
    {
        var query = new GetDeliveryDetailsQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddDelivery(AddDeliveryCommand command)
    {
        var deliveryId = await _mediator.Send(command);

        return Ok(deliveryId);
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteDelivery(Guid id)
    {
        await _mediator.Send(new CompleteDeliveryCommand(id));

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDelivery(Guid id)
    {
        await _mediator.Send(new DeleteDeliveryCommand(id));

        return NoContent();
    }
}
