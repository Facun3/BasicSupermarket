using System.Linq.Expressions;
using BasicSupermarket.Domain.Communication;
using BasicSupermarket.Domain.Services;
using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Mapping;
using BasicSupermarket.Domain.Services.Communication;
using BasicSupermarket.Domain.Repositories;
using BasicSupermarket.Repositories;
using Microsoft.EntityFrameworkCore;


namespace BasicSupermarket.Services;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ILogger<ProductService> logger): IProductService
{
    
    public async Task<QueryResponseDto<ProductResponseDto>> ListAsync(ProductQuery query)
    {
        IQueryable<Product> queryable = productRepository.GetQuery().Include(product => product.Category);;
        
        if (!string.IsNullOrEmpty(query.SearchFor))
        {
            queryable = queryable.Where(product => product.Name.Contains(query.SearchFor) || product.Description.Contains(query.SearchFor) || product.Category.Name.Contains(query.SearchFor));
        }
        
        if (query.CategoryId.HasValue)
        {
            queryable = queryable.Where(product => product.CategoryId == query.CategoryId.Value);
        }
        
        if (query.MinPrice.HasValue)
        {
            queryable = queryable.Where(product => product.Price >= query.MinPrice.Value);
        }

        if (query.MaxPrice.HasValue)
        {
            queryable = queryable.Where(product => product.Price <= query.MaxPrice.Value);
        }
        
        // Obtener los productos filtrados de forma asíncrona (sin ejecutarlo aún)
        var queriedProducts = await queryable.ToListAsync();

        // Aplicar la paginación (solo después de obtener la lista completa de productos)
        var paginatedProducts = queriedProducts
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();
        
        int totalProducts = queryable.Count();

        // Mapear los productos a DTOs para la respuesta
        var response = new QueryResponseDto<ProductResponseDto>
        {
            Page = query.Page,
            PageSize = query.PageSize,
            Total = totalProducts,
            Result = ProductMapper.FromProductToProductResponseDto(paginatedProducts)
        };
        
        return response;
    }

    public async Task<Response<ProductResponseDto>> GetByIdAsync(int id)
    {
        try
        {
            var product = await productRepository.GetQuery().FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return new Response<ProductResponseDto>("Product not found");
            }
            return new Response<ProductResponseDto>(ProductMapper.FromProductToProductResponseDto(product));
        }
        catch (Exception ex)
        {
            String err = ex.Message;
            logger.LogError(ex, ex.Message);
            return new Response<ProductResponseDto>($"Error getting product: {err}");
        }
        
    }

    public async Task<Response<ProductResponseDto>> CreateAsync(CreateProductRequestDto product)
    {
        try
        {
            IQueryable<Product> query = productRepository.GetQuery();
            var existingCategory = await query.FirstOrDefaultAsync(prod => prod.Name == product.Name);
            if (existingCategory != null)
            {
                return new Response<ProductResponseDto>("Product Name Already Exists");
            }
            var newProduct = ProductMapper.FromCreateProductRequestDtoToProduct(product);
            await productRepository.AddAsync(newProduct);
            await unitOfWork.CompleteAsync();
            return new Response<ProductResponseDto>(ProductMapper.FromProductToProductResponseDto(newProduct));
        }
        catch (Exception ex)
        {
            String err = ex.Message;
            logger.LogError(ex, ex.Message);
            return new Response<ProductResponseDto>($"Error creating product: {err}");
        }
    }

    public async Task<Response<ProductResponseDto>> UpdateAsync(int id, UpdateProductRequestDto productRequestDto)
    {
        var productExist = await productRepository.GetQuery().FirstOrDefaultAsync(p => p.Id == id);
        if (productExist == null)
        {
            return new Response<ProductResponseDto>("Product not found");
        }
        try
        {
            var updateProduct = ProductMapper.FromUpdateProductRequestDtoToProduct(id, productRequestDto);
            productRepository.Update(updateProduct);
            await unitOfWork.CompleteAsync();
            return new Response<ProductResponseDto>(ProductMapper.FromProductToProductResponseDto(updateProduct));
        }
        catch (Exception ex)
        {
            String err = ex.Message;
            logger.LogError(ex, $"Error updating product: {err}");
            return new Response<ProductResponseDto>($"Error updating product: {err}");
        }
    }

    public async Task<Response<ProductResponseDto>> DeleteAsync(int id)
    {
        var productExist = await productRepository.GetQuery().FirstOrDefaultAsync(p => p.Id == id);
        if (productExist == null)
        {
            return new Response<ProductResponseDto>("Product not found");
        }
        try
        {
            productRepository.Delete(productExist);
            await unitOfWork.CompleteAsync();
            return new Response<ProductResponseDto>(ProductMapper.FromProductToProductResponseDto(productExist));
        }
        catch (Exception ex)
        {
            String err = ex.Message;
            logger.LogError(ex, "Error deleting product.");
            return new Response<ProductResponseDto>($"Error deleting product: {err}");
        }
    }
}