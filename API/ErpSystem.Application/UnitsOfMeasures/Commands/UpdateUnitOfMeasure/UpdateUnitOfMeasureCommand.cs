using MediatR;

namespace ErpSystem.Application.UnitsOfMeasures.Commands.UpdateUnitOfMeasure;

public record UpdateUnitOfMeasureCommand(Guid Id, string Name) : IRequest;
