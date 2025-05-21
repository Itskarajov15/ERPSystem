using ErpSystem.Application.PaymentMethods.Commands.CreatePaymentMethod;
using ErpSystem.Application.PaymentMethods.Commands.DeletePaymentMethod;
using ErpSystem.Application.PaymentMethods.Commands.UpdatePaymentMethod;
using ErpSystem.Application.PaymentMethods.DTOs;
using ErpSystem.Application.PaymentMethods.Queries.GetPaymentMethodById;
using ErpSystem.Application.PaymentMethods.Queries.GetPaymentMethods;
using ErpSystem.Domain.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

public class PaymentMethodController : BaseController
{
    public PaymentMethodController(IMediator mediator)
        : base(mediator) { }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentMethodDto>>> GetAll(
        [FromQuery] PaginationParams paginationParams
    )
    {
        var paymentMethods = await _mediator.Send(new GetPaymentMethodsQuery(paginationParams));
        return Ok(paymentMethods);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentMethodDto>> GetById(Guid id)
    {
        var paymentMethod = await _mediator.Send(new GetPaymentMethodByIdQuery(id));
        return Ok(paymentMethod);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreatePaymentMethodCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdatePaymentMethodCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID in the URL does not match the ID in the request body");
        }

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeletePaymentMethodCommand(id));
        return NoContent();
    }
}
