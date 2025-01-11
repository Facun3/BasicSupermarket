namespace BasicSupermarket.Domain.Repositories;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetQuery();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}