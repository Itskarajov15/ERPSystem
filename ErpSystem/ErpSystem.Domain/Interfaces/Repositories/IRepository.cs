using ErpSystem.Domain.Abstractions;

namespace ErpSystem.Domain.Interfaces.Repositories;

public interface IRepository<T>
    where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);

    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);
}
