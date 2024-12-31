using BasicSupermarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicSupermarket.Repositories;

public interface ICategoryRepository: IRepository<Category>
{
    
}