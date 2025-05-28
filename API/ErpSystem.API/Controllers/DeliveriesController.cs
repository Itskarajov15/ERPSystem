using ErpSystem.Application.Deliveries.Commands.AddDelivery;
using ErpSystem.Application.Deliveries.Commands.CompleteDelivery;
using ErpSystem.Application.Deliveries.Commands.DeleteDelivery;
using ErpSystem.Application.Deliveries.Commands.StartDeliveryProgress;
using ErpSystem.Application.Deliveries.Queries.GetDeliveries;
using ErpSystem.Application.Deliveries.Queries.GetDeliveryDetails;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

[Route("api/deliveries")]
public class DeliveriesController : BaseController
{
    public DeliveriesController(IMediator mediator)
        : base(mediator) { }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] DeliveryFilters? filters = null
    )
    {
        var query = new GetDeliveriesQuery(paginationParams, filters);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetDeliveryDetailsQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(AddDeliveryCommand command)
    {
        var deliveryId = await _mediator.Send(command);

        return Ok(deliveryId);
    }

    [HttpPut("{id}/start-progress")]
    public async Task<IActionResult> StartProgress(Guid id)
    {
        await _mediator.Send(new StartDeliveryProgressCommand(id));

        return NoContent();
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        await _mediator.Send(new CompleteDeliveryCommand(id));

        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteDeliveryCommand(id));

        return NoContent();
    }
}
