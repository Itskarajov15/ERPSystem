using ErpSystem.Application.Common.Constants;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        var products = await _repository
            .AllReadOnly<Product>()
            .Where(p => p.UnitOfMeasureId == request.Id)
            .ToListAsync();

        if (products.Any())
        {
            throw new InvalidOperationException(ProductErrorKeys.ProductUsesUnitOfMeasure);
        }

        await _repository.SoftDeleteById<UnitOfMeasure>(request.Id);
        await _repository.SaveChangesAsync();
    }
}
