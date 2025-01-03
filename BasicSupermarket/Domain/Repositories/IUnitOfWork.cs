namespace BasicSupermarket.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}