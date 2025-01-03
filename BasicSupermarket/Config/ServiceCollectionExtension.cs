using System.Reflection;
using BasicSupermarket.Domain.Services;
using BasicSupermarket.Persistence.Repositories;
using BasicSupermarket.Repositories;
using BasicSupermarket.Services;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace BasicSupermarket.Config;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        //Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        //Services
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        
        return services;
    }
    
    
    
    
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        // Automatically register all validators from the assembly where this method is called
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // Optionally add FluentValidation's MVC integration
        services.AddFluentValidationAutoValidation();

        return services;
    }

}