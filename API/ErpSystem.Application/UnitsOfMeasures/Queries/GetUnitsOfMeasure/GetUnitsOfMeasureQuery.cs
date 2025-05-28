using ErpSystem.Application.UnitsOfMeasures.DTOs;
using ErpSystem.Domain.Common.Pagination;
using MediatR;

namespace ErpSystem.Application.UnitsOfMeasures.Queries.GetUnitsOfMeasure;

public record GetUnitsOfMeasureQuery(PaginationParams PaginationParams)
    : IRequest<PageResult<UnitOfMeasureDto>>;
