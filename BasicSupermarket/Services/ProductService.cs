using BasicSupermarket.Domain.Communication;
using BasicSupermarket.Domain.Services;
using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Dto.Product;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Services.Communication;
using BasicSupermarket.Domain.Repositories;
using BasicSupermarket.Mapping;
using Microsoft.EntityFrameworkCore;


namespace BasicSupermarket.Services;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ILogger<ProductService> logger): IProductService
{
    
    public async Task<QueryResponseDto<ProductResponseDto>> ListAsync(ProductQuery query)
    {
        IQueryable<Product> queryable = productRepository.GetQuery().Include(product => product.Category);;
        
        if (!string.IsNullOrEmpty(query.SearchFor))
            queryable = queryable.Where(product => product.Name.Contains(query.SearchFor) || product.Description.Contains(query.SearchFor) || product.Category.Name.Contains(query.SearchFor));
        
        if (query.CategoryId.HasValue)
            queryable = queryable.Where(product => product.CategoryId == query.CategoryId.Value);
        
        if (query.MinPrice.HasValue)
            queryable = queryable.Where(product => product.Price >= query.MinPrice.Value);

        if (query.MaxPrice.HasValue)
            queryable = queryable.Where(product => product.Price <= query.MaxPrice.Value);
        
        var queriedProducts = await queryable.ToListAsync();
        
        var paginatedProducts = queriedProducts
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();
        
        int totalProducts = queryable.Count();
        
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
        var product = await productRepository.GetQuery().FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
            return Response<ProductResponseDto>.FailureResponse("Product not found");
        return Response<ProductResponseDto>.SuccessResponse(ProductMapper.FromProductToProductResponseDto(product));
    }

    public async Task<Response<ProductResponseDto>> CreateAsync(CreateProductRequestDto newProductDto)
    {
        try
        {
            IQueryable<Product> query = productRepository.GetQuery();

            var existingCategory = await query.FirstOrDefaultAsync(prod => prod.Name == newProductDto.Name);
            if (existingCategory != null)
                return Response<ProductResponseDto>.FailureResponse("Product already exists");

            var newProduct = Product.Create(newProductDto.Name, newProductDto.Description, newProductDto.Price,
                newProductDto.Quantity, newProductDto.CategoryId);
            if (newProductDto.ImageUrl != String.Empty)
            {
                newProduct.UpdateImageUrl(newProductDto.ImageUrl);
            }

            await productRepository.AddAsync(newProduct);
            await unitOfWork.CompleteAsync();
            return Response<ProductResponseDto>.SuccessResponse(
                ProductMapper.FromProductToProductResponseDto(newProduct));
        }

        catch (Exception ex)
        {
            return Response<ProductResponseDto>.FailureResponse("Failed to create product");
        }
    }

    public async Task<Response<ProductResponseDto>> UpdateAsync(int id, UpdateProductRequestDto productRequestDto)
    {
        var productExist = await productRepository.GetQuery().FirstOrDefaultAsync(p => p.Id == id);
        if (productExist == null)
            return Response<ProductResponseDto>.FailureResponse("Product not found");
        
        productExist.UpdateDetails(
            productRequestDto.Name,
            productRequestDto.Description,
            productRequestDto.Price,
            productRequestDto.ImageUrl
        );
        if(productRequestDto.CategoryId.HasValue)
            productExist.UpdateCategory(productRequestDto.CategoryId.Value);
        productRepository.Update(productExist);
        await unitOfWork.CompleteAsync();
        var responseDto = ProductMapper.FromProductToProductResponseDto(productExist);
        return Response<ProductResponseDto>.SuccessResponse(responseDto);
    }

    public async Task<Response<ProductResponseDto>> DeleteAsync(int id)
    {
        var productExist = await productRepository.GetQuery().FirstOrDefaultAsync(p => p.Id == id);
        if (productExist == null)
            return Response<ProductResponseDto>.FailureResponse("Product not found");
        productRepository.Delete(productExist);
        await unitOfWork.CompleteAsync();
        return Response<ProductResponseDto>.SuccessResponse(ProductMapper.FromProductToProductResponseDto(productExist));
    }
}