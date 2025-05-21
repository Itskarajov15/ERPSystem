using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.UnitsOfMeasures.Commands.UpdateUnitOfMeasure;

internal class UpdateUnitOfMeasureCommandHandler : IRequestHandler<UpdateUnitOfMeasureCommand>
{
    private readonly IRepository _repository;

    public UpdateUnitOfMeasureCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        UpdateUnitOfMeasureCommand request,
        CancellationToken cancellationToken
    )
    {
        var unitOfMeasure = await _repository.GetByIdAsync<UnitOfMeasure>(request.Id);

        if (unitOfMeasure == null)
        {
            throw new NotFoundException(nameof(UnitOfMeasure), request.Id);
        }

        unitOfMeasure.Name = request.Name;

        await _repository.SaveChangesAsync();
    }
}
