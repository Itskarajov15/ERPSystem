using ErpSystem.Application.PaymentMethods.Commands.CreatePaymentMethod;
using ErpSystem.Application.PaymentMethods.Commands.DeletePaymentMethod;
using ErpSystem.Application.PaymentMethods.Commands.UpdatePaymentMethod;
using ErpSystem.Application.PaymentMethods.Queries.GetPaymentMethodById;
using ErpSystem.Application.PaymentMethods.Queries.GetPaymentMethods;
using ErpSystem.Domain.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

[Route("api/payment-methods")]
public class PaymentMethodsController : BaseController
{
    public PaymentMethodsController(IMediator mediator)
        : base(mediator) { }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
    {
        var paymentMethods = await _mediator.Send(new GetPaymentMethodsQuery(paginationParams));

        return Ok(paymentMethods);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var paymentMethod = await _mediator.Send(new GetPaymentMethodByIdQuery(id));

        return Ok(paymentMethod);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreatePaymentMethodCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(id);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePaymentMethodCommand command)
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
        await _mediator.Send(new DeletePaymentMethodCommand(id));

        return NoContent();
    }
}
