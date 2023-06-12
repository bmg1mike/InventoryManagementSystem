using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class RepositoryBase<T> : IAsyncRepository<T> where T : class
{
    protected readonly InventoryContext _dbContext;

    public RepositoryBase(InventoryContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResponse<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        var data = await PaginatedList<T>.CreateAsync(_dbContext.Set<T>(), pageNumber, pageSize);
        var result = new PaginationResponse<T>()
        {
            CurrentPage = data.CurrentPage,
            Data = data,
            PageSize = data.PageSize,
            TotalCount = data.TotalCount,
            TotalPages = data.TotalPages
        };
        return result;
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();
        if (disableTracking) query = query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();
        if (disableTracking) query = query.AsNoTracking();

        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(string id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await Task.CompletedTask;
    }
}