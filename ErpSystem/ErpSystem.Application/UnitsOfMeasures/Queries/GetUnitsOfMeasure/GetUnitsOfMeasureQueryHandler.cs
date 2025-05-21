using ErpSystem.Application.UnitsOfMeasures.DTOs;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.UnitsOfMeasures.Queries.GetUnitsOfMeasure;

internal class GetUnitsOfMeasureQueryHandler
    : IRequestHandler<GetUnitsOfMeasureQuery, PageResult<UnitOfMeasureDto>>
{
    private readonly IRepository _repository;

    public GetUnitsOfMeasureQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<UnitOfMeasureDto>> Handle(
        GetUnitsOfMeasureQuery request,
        CancellationToken cancellationToken
    )
    {
        var unitsOfMeasure = await _repository.GetPaginatedAsync<UnitOfMeasure, UnitOfMeasureDto>(
            request.PaginationParams,
            x => x.Select(x => new UnitOfMeasureDto() { Id = x.Id, Name = x.Name })
        );

        return unitsOfMeasure;
    }
}
