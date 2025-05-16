using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Common.Pagination;

namespace ErpSystem.Domain.Interfaces;

public interface IRepository
{
    IQueryable<T> All<T>()
        where T : class;

    IQueryable<T> AllReadOnly<T>()
        where T : class;

    Task AddAsync<T>(T entity)
        where T : class;

    Task AddRangeAsync<T>(IEnumerable<T> entities)
        where T : class;

    Task<T?> GetByIdAsync<T>(Guid id)
        where T : class;

    void Delete<T>(T entity)
        where T : class;

    Task DeleteById<T>(Guid id)
        where T : class;

    Task<int> SaveChangesAsync();

    void DeleteRange<T>(IEnumerable<T> entities)
        where T : class;

    void SoftDelete<T>(T entity)
        where T : BaseEntity;

    Task SoftDeleteById<T>(object id)
        where T : BaseEntity;

    void UnDelete<T>(T entity)
        where T : BaseEntity;

    Task<IEnumerable<T>> GetByIdsAsync<T>(IEnumerable<Guid> ids)
        where T : BaseEntity;

    Task<PageResult<TDto>> GetPaginatedAsync<TEntity, TDto>(
        PaginationParams pageParams,
        Func<IQueryable<TEntity>, IQueryable<TDto>> selector,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? filterBy = null
    )
        where TEntity : class;
}
