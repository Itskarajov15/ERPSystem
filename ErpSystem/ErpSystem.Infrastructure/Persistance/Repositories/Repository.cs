using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Interfaces;

namespace ErpSystem.Infrastructure.Persistance.Repositories;

public class Repository : IRepository
{
    public Task AddAsync<T>(T entity)
        where T : class
    {
        throw new NotImplementedException();
    }

    public Task AddRangeAsync<T>(IEnumerable<T> entities)
        where T : class
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> All<T>()
        where T : class
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> AllReadOnly<T>()
        where T : class
    {
        throw new NotImplementedException();
    }

    public void Delete<T>(T entity)
        where T : class
    {
        throw new NotImplementedException();
    }

    public Task DeleteById<T>(Guid id)
        where T : class
    {
        throw new NotImplementedException();
    }

    public void DeleteRange<T>(IEnumerable<T> entities)
        where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetByIdAsync<T>(Guid id)
        where T : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetByIdsAsync<T>(IEnumerable<Guid> ids)
        where T : BaseEntity
    {
        throw new NotImplementedException();
    }

    public Task<PageResult<TDto>> GetPaginatedAsync<TEntity, TDto>(
        PaginationParams pageParams,
        Func<IQueryable<TEntity>, IQueryable<TDto>> selector,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? filterBy = null
    )
        where TEntity : class
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public void SoftDelete<T>(T entity)
        where T : BaseEntity
    {
        throw new NotImplementedException();
    }

    public Task SoftDeleteById<T>(object id)
        where T : BaseEntity
    {
        throw new NotImplementedException();
    }

    public void UnDelete<T>(T entity)
        where T : BaseEntity
    {
        throw new NotImplementedException();
    }
}
