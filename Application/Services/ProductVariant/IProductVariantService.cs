using DTOs.Product;

namespace Application.Services.ProductVariant
{
    public interface IProductVariantService
    {
        Task<ProductDto> GetParentAsync(Guid id);
        Task<PaginationOutputDto<ProductVariantDto>> GetAllVariantsAsync(PaginationInputDto pagination, Guid parentProductId);
        Task<ProductVariantDto> GetVariantAsync(Guid id);

        Task<ProductVariantDto> CreateVariantAsync(ProductVariantDto variant);

        Task UpdateVariantAsync(ProductVariantDto variant);

        Task RemoveVariantAsync(Guid id);

        // Other functions could be added depending on requirements,
    }
}
