using ErpSystem.Application.Payments.Commands.RecordPayment;
using ErpSystem.Application.Payments.Queries.GetPaymentById;
using ErpSystem.Application.Payments.Queries.GetPayments;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

[Route("api/payments")]
public class PaymentsController : BaseController
{
    public PaymentsController(IMediator mediator)
        : base(mediator) { }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] PaymentFilters? paymentFilters
    )
    {
        var query = new GetPaymentsQuery(paginationParams, paymentFilters);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetPaymentByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("record")]
    public async Task<IActionResult> RecordPayment(RecordPaymentCommand command)
    {
        await _mediator.Send(command);

        return NoContent();
    }
}
