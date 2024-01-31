using DTOs.Product;

namespace Application.Services.Product
{
    public interface IProductService
    {
        Task<PaginationOutputDto<ProductDto>> GetAllProductsAsync(PaginationInputDto pagination);
        Task<ProductDto> GetProductAsync(Guid id);

        Task<ProductDto> CreateProductAsync(ProductDto product);

        Task UpdateProductAsync(ProductDto product);

        Task RemoveProductAsync(Guid id);

        // Other functions could be added depending on requirements, for example dealing with product image
    }
}
