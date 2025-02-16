using BasicSupermarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicSupermarket.Domain.Repositories;

public interface ICategoryRepository: IRepository<Category>
{
    //If there's some specific function needed for categories, just add it here.
}