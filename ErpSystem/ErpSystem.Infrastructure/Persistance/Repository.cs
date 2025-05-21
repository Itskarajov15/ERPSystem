using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Infrastructure.Persistance;

public class Repository : IRepository
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync<T>(T entity)
        where T : class
    {
        await _context.AddAsync(entity);
    }

    public async Task AddRangeAsync<T>(IEnumerable<T> entities)
        where T : class
    {
        await _context.AddRangeAsync(entities);
    }

    public IQueryable<T> All<T>()
        where T : class
    {
        return _context.Set<T>();
    }

    public IQueryable<T> AllReadOnly<T>()
        where T : class
    {
        return _context.Set<T>().AsNoTracking();
    }

    public void Delete<T>(T entity)
        where T : class
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task DeleteById<T>(Guid id)
        where T : class
    {
        var entity = await GetByIdAsync<T>(id);

        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
        }
    }

    public void DeleteRange<T>(IEnumerable<T> entities)
        where T : class
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public async Task<T?> GetByIdAsync<T>(Guid id)
        where T : class
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetByIdsAsync<T>(IEnumerable<Guid> ids)
        where T : BaseEntity
    {
        return await _context.Set<T>().Where(e => ids.Contains(e.Id)).ToListAsync();
    }

    public async Task<PageResult<TDto>> GetPaginatedAsync<TEntity, TDto>(
        PaginationParams pageParams,
        Func<IQueryable<TEntity>, IQueryable<TDto>> selector,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? filterBy = null
    )
        where TEntity : class
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (filterBy != null)
        {
            query = filterBy(query);
        }

        var totalCount = await query.CountAsync();

        var projectedQuery = selector(query);

        var result = await projectedQuery
            .Skip((pageParams.Page - 1) * pageParams.PageSize)
            .Take(pageParams.PageSize)
            .ToListAsync();

        return new PageResult<TDto>()
        {
            TotalCount = totalCount,
            CurrentPage = pageParams.Page,
            PageSize = pageParams.PageSize,
            Items = result,
        };
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void SoftDelete<T>(T entity)
        where T : BaseEntity
    {
        entity.IsDeleted = true;
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task SoftDeleteById<T>(Guid id)
        where T : BaseEntity
    {
        var entity = await GetByIdAsync<T>(id);

        if (entity == null)
        {
            throw new KeyNotFoundException($"{nameof(T)} with ID '{id}' was not found.");
        }

        entity.IsDeleted = true;
    }

    public void UnDelete<T>(T entity)
        where T : BaseEntity
    {
        entity.IsDeleted = false;
        _context.Entry(entity).State = EntityState.Modified;
    }
}
