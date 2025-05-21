using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.UnitsOfMeasures.Commands.CreateUnitOfMeasure;

internal class CreateUnitOfMeasureCommandHandler : IRequestHandler<CreateUnitOfMeasureCommand, Guid>
{
    private readonly IRepository _repository;

    public CreateUnitOfMeasureCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateUnitOfMeasureCommand request,
        CancellationToken cancellationToken
    )
    {
        var unitOfMeasure = new UnitOfMeasure() { Name = request.Name };

        await _repository.AddAsync(unitOfMeasure);
        await _repository.SaveChangesAsync();

        return unitOfMeasure.Id;
    }
}
