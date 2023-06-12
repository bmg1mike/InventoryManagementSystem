using System.Linq.Expressions;

namespace Domain;

public interface IAsyncRepository<T> where T : class
{
    Task<PaginationResponse<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                    string includeString = null,
                                    bool disableTracking = true);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                   List<Expression<Func<T, object>>> includes = null,
                                   bool disableTracking = true);
    Task<T> GetByIdAsync(string id);
    Task<T> AddAsync(T entity);
    Task DeleteAsync(T entity);
}