using MediatR;

namespace ErpSystem.Application.UnitsOfMeasures.Commands.CreateUnitOfMeasure;

public record CreateUnitOfMeasureCommand(string Name) : IRequest<Guid>;
