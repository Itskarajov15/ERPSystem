using MediatR;

namespace ErpSystem.Application.UnitsOfMeasures.Commands.DeleteUnitOfMeasure;

public record DeleteUnitOfMeasureCommand(Guid Id) : IRequest;
