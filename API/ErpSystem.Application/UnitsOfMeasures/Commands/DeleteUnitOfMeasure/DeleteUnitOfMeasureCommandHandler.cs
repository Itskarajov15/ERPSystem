using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.UnitsOfMeasures.Commands.DeleteUnitOfMeasure;

internal class DeleteUnitOfMeasureCommandHandler : IRequestHandler<DeleteUnitOfMeasureCommand>
{
    private readonly IRepository _repository;

    public DeleteUnitOfMeasureCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        DeleteUnitOfMeasureCommand request,
        CancellationToken cancellationToken
    )
    {
        await _repository.SoftDeleteById<UnitOfMeasure>(request.Id);
        await _repository.SaveChangesAsync();
    }
}
