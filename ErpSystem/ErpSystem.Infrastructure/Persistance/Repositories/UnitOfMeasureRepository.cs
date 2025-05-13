using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces.Repositories;

namespace ErpSystem.Infrastructure.Persistance.Repositories;

public class UnitOfMeasureRepository : Repository<UnitOfMeasure>, IUnitOfMeasureRepository
{
    public UnitOfMeasureRepository(ApplicationDbContext context)
        : base(context) { }
}
