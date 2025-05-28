using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Application.UnitsOfMeasures.DTOs;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.UnitsOfMeasures.Queries.GetUnitOfMeasureById;

internal class GetUnitOfMeasureIdQueryHandler
    : IRequestHandler<GetUnitOfMeasureByIdQuery, UnitOfMeasureDto>
{
    private readonly IRepository _repository;

    public GetUnitOfMeasureIdQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<UnitOfMeasureDto> Handle(
        GetUnitOfMeasureByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var unitOfMeasure = await _repository.GetByIdAsync<UnitOfMeasure>(request.Id);

        if (unitOfMeasure is null)
        {
            throw new NotFoundException(nameof(UnitOfMeasure), request.Id);
        }

        return new UnitOfMeasureDto() { Id = unitOfMeasure.Id, Name = unitOfMeasure.Name };
    }
}
