using System.Linq.Expressions;

namespace BasicSupermarket.Repositories;

public interface IRepository<T> where T : class
{
    Task<IQueryable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> UpdateAsync(T entity);
    Task<int> DeleteAsync(int id);
}