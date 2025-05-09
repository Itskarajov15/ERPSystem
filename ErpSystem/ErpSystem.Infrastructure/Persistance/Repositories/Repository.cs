using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Infrastructure.Persistance.Repositories;

public class Repository<T> : IRepository<T>
    where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(T entity) => _context.Set<T>().Add(entity);

    public void Delete(T entity)
    {
        entity.IsDeleted = true;
        Update(entity);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken) =>
        await _context.Set<T>().Where(e => !e.IsDeleted).ToListAsync(cancellationToken);

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context
            .Set<T>()
            .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted, cancellationToken);

    public void Update(T entity) => _context.Entry(entity).State = EntityState.Modified;
}
