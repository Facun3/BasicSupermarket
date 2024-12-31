using System.Linq.Expressions;
using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Mapping;
using BasicSupermarket.Repositories;


namespace BasicSupermarket.Services;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;
    
    public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }
    
    public async Task<IEnumerable<ProductResponseDto>> GetProducts(string? name , int? page = 1, int? pageSize = 10)
    {
        try
        {
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            
            Expression<Func<Product, bool>> predicate = product => 
                string.IsNullOrEmpty(name) || product.Name.Contains(name) || product.Description.Contains(name);

            IQueryable<Product> queriedProducts = await _productRepository.SearchAsync(predicate);
            
            // Apply pagination
            List<Product> paginatedProducts = queriedProducts
                .Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value).ToList();
            var response = ProductMapper.FromProductToProductResponseDto(paginatedProducts);
            return response;

        }

        catch (Exception ex)
        {
            String err = ex.Message;
            _logger.LogError(ex, ex.Message);
            throw new ApplicationException(err);
        }
    }

    public async Task<ProductResponseDto?> GetProduct(int id)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? null : ProductMapper.FromProductToProductResponseDto(product);
        }
        catch (Exception ex)
        {
            String err = ex.Message;
            _logger.LogError(ex, ex.Message);
            throw new ApplicationException(err);
        }
        
    }

    public async Task<ProductResponseDto> PostProduct(CreateProductRequestDto product)
    {
        try
        {
            var newProduct = await _productRepository.AddAsync(ProductMapper.FromCreateProductRequestDtoToProduct(product));
            return ProductMapper.FromProductToProductResponseDto(newProduct);
        }
        catch (Exception ex)
        {
            String err = ex.Message;
            _logger.LogError(ex, ex.Message);
            throw new ApplicationException(err);
        }
    }

    public async Task<ProductResponseDto> PutProduct(int id, UpdateProductRequestDto productRequestDto)
    {
        try
        {
            //TODO: Add some validations here
            Product updateProduct = await _productRepository.UpdateAsync(ProductMapper.FromUpdateProductRequestDtoToProduct(id, productRequestDto));
            return ProductMapper.FromProductToProductResponseDto(updateProduct);
        }
        catch (Exception ex)
        {
            String err = ex.Message;
            _logger.LogError(ex, ex.Message);
            throw new ApplicationException(err);
        }
    }

    public Task<int> DeleteProduct(int id)
    {
        try
        {
            return _productRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            String err = ex.Message;
            _logger.LogError(ex, ex.Message);
            throw new ApplicationException(err);
        }
    }
}