using System.Linq.Expressions;
using BasicSupermarket.Domain;
using BasicSupermarket.Domain.Communication;
using BasicSupermarket.Domain.Services;
using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Mapping;
using BasicSupermarket.Repositories;


namespace BasicSupermarket.Services;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductService> _logger;
    
    public ProductService(IProductRepository productRepository, ILogger<ProductService> logger, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<IEnumerable<ProductResponseDto>> ListAsync(ProductQuery query)
    {
        Expression<Func<Product, bool>> predicate = product => 
            string.IsNullOrEmpty(query.SearchFor) || product.Name.Contains(query.SearchFor) || product.Description.Contains(query.SearchFor);
            
        IEnumerable<Product> queriedProducts = await _productRepository.SearchAsync(predicate);
            
        // Apply pagination
        List<Product> paginatedProducts = queriedProducts
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize).ToList();
        var response = ProductMapper.FromProductToProductResponseDto(paginatedProducts);
        return response;
    }

    public async Task<Response<ProductResponseDto>> GetByIdAsync(int id)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return new Response<ProductResponseDto>("Product not found");
            }
            return new Response<ProductResponseDto>(ProductMapper.FromProductToProductResponseDto(product));
        }
        catch (Exception ex)
        {
            String err = ex.Message;
            _logger.LogError(ex, ex.Message);
            return new Response<ProductResponseDto>($"Error getting product: {err}");
        }
        
    }

    public async Task<Response<ProductResponseDto>> CreateAsync(CreateProductRequestDto product)
    {
        try
        {
            var newProduct = ProductMapper.FromCreateProductRequestDtoToProduct(product);
            await _productRepository.AddAsync(newProduct);
            await _unitOfWork.CompleteAsync();
            return new Response<ProductResponseDto>(ProductMapper.FromProductToProductResponseDto(newProduct));
        }
        catch (Exception ex)
        {
            String err = ex.Message;
            _logger.LogError(ex, ex.Message);
            return new Response<ProductResponseDto>($"Error creating product: {err}");
        }
    }

    public async Task<Response<ProductResponseDto>> UpdateAsync(int id, UpdateProductRequestDto productRequestDto)
    {
        var productExist = await _productRepository.GetByIdAsync(id);
        if (productExist == null)
        {
            return new Response<ProductResponseDto>("Product not found");
        }
        try
        {
            var updateProduct = ProductMapper.FromUpdateProductRequestDtoToProduct(id, productRequestDto);
            _productRepository.Update(updateProduct);
            await _unitOfWork.CompleteAsync();
            return new Response<ProductResponseDto>(ProductMapper.FromProductToProductResponseDto(updateProduct));
        }
        catch (Exception ex)
        {
            String err = ex.Message;
            _logger.LogError(ex, $"Error updating product: {err}");
            return new Response<ProductResponseDto>($"Error updating product: {err}");
        }
    }

    public async Task<Response<ProductResponseDto>> DeleteAsync(int id)
    {
        var productExist = await _productRepository.GetByIdAsync(id);
        if (productExist == null)
        {
            return new Response<ProductResponseDto>("Product not found");
        }
        try
        {
            _productRepository.Delete(productExist);
            await _unitOfWork.CompleteAsync();
            return new Response<ProductResponseDto>(ProductMapper.FromProductToProductResponseDto(productExist));
        }
        catch (Exception ex)
        {
            String err = ex.Message;
            _logger.LogError(ex, "Error deleting product.");
            return new Response<ProductResponseDto>($"Error deleting product: {err}");
        }
    }
}