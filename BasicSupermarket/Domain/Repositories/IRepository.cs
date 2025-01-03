using System.Linq.Expressions;

namespace BasicSupermarket.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    void Update(T entity);
    void Delete(T entity);
}