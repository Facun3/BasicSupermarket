using BasicSupermarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicSupermarket.Domain.Repositories;

public interface ICategoryRepository: IRepository<Category>
{
    
}