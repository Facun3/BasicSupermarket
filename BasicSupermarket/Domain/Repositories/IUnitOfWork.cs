namespace BasicSupermarket.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}