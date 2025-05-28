using ErpSystem.Application.UnitsOfMeasures.DTOs;
using MediatR;

namespace ErpSystem.Application.UnitsOfMeasures.Queries.GetUnitOfMeasureById;

public record GetUnitOfMeasureByIdQuery(Guid Id) : IRequest<UnitOfMeasureDto>;
