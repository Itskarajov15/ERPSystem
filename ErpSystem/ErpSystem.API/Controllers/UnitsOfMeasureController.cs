using ErpSystem.Application.UnitsOfMeasures.Commands.CreateUnitOfMeasure;
using ErpSystem.Application.UnitsOfMeasures.Commands.DeleteUnitOfMeasure;
using ErpSystem.Application.UnitsOfMeasures.Commands.UpdateUnitOfMeasure;
using ErpSystem.Application.UnitsOfMeasures.Queries.GetUnitOfMeasureById;
using ErpSystem.Application.UnitsOfMeasures.Queries.GetUnitsOfMeasure;
using ErpSystem.Domain.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

[Route("api/unitsOfMeasure")]
[ApiController]
public class UnitsOfMeasureController : BaseController
{
    public UnitsOfMeasureController(IMediator mediator)
        : base(mediator) { }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
    {
        var units = await _mediator.Send(new GetUnitsOfMeasureQuery(paginationParams));

        return Ok(units);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var unit = await _mediator.Send(new GetUnitOfMeasureByIdQuery(id));

        return Ok(unit);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUnitOfMeasureCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUnitOfMeasureCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID in the URL does not match the ID in the request body");
        }

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteUnitOfMeasureCommand(id));

        return NoContent();
    }
}
