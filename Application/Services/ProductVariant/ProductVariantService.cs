using DTOs.Product;

namespace Application.Services.ProductVariant
{
    using Application.Services;
    using Domain.Entities;
    using Domain.Enums.EntitiesEnums;
    using System.Linq.Expressions;

    public class ProductVariantService : IProductVariantService
    {
        private IUnitOfWork _uow { get; }
        public ProductVariantService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ProductVariantDto> CreateVariantAsync(ProductVariantDto variant)
        {
            var parentProduct = await _uow.ProductRepository
                .FirstOrDefaultAsync(p => p.Id.ToString() == variant.ParentProductId);

            var dbVariant = await _uow.VariantRepository
                    .FirstOrDefaultAsync
                    (
                        v =>
                        // attribute value (or variant) equality
                        v.Value.ToLower() == variant.Value.ToLower()
                        // attribute name equality
                        && v.Attribute.Name.ToLower() == variant.Attribute.ToLower()
                    );

            if (dbVariant == null)
            {
                AttributeTypeEnum attributeType = HelperMethod.MapAttributeTypeToEnum(variant.AttributeType);
                var newAttribute = await _uow.AttributeRepository.AddAsync(new Domain.Entities.Attribute
                {
                    Name = variant.Attribute,
                    Type = attributeType
                });
                dbVariant = await _uow.VariantRepository.AddAsync(new Variant
                {
                    Attribute = newAttribute,
                    Value = variant.Value
                });
            }

            ProductVariant newProductVariant = new ProductVariant
            {
                Product = parentProduct,
                Variant = dbVariant
            };

            await _uow.ProductVariantRepository.AddAsync(newProductVariant);

            foreach (var image in variant.Images)
            {
                var dbImag = parentProduct.Images.FirstOrDefault(img => img.Path == image.Path);
                if (dbImag != null)
                    await _uow.VariantImageRepository.AddAsync(new VariantImage
                    {
                        Image = dbImag,
                        ProductVariant = newProductVariant
                    });
            }

            await _uow.SaveChangesAsync();
            return await GetVariantAsync(newProductVariant.Id);
        }


        public async Task<PaginationOutputDto<ProductVariantDto>> GetAllVariantsAsync(PaginationInputDto pagination, Guid parentProductId)
        {
            var parentProduct = await _uow.ProductRepository.GetByIdAsync(parentProductId);

            int totalCount = parentProduct.ProductVariants.Count();
            int page = pagination?.Page ?? 1;
            int? pageSize = pagination?.PageSize ?? null;

            var productVariants = await _uow.ProductVariantRepository
                .GetAllAsync
                (
                    filter:
                        pv => pv.Product.Id == parentProductId &&
                            (
                                (!String.IsNullOrEmpty(pagination.Filter)) ?
                                pv.Variant.Value.ToLower().Contains(pagination.Filter) : true
                            ),
                    orderBy:
                         (!String.IsNullOrEmpty(pagination.SortColumn)) ?
                        (
                            // if user specify [asc] order
                            pv => (pagination.SortOrder.ToLower() == "asc") ?
                            pv.OrderBy(MapSortColumnToVariantProperty(pagination.SortColumn)) :
                            // else user specify [desc] order (Also, the default order is desc if the user does not specify)
                            pv.OrderByDescending(MapSortColumnToVariantProperty(pagination.SortColumn))
                        )
                        : null,

                    take:
                        (pagination.PageSize > 0) ? pagination.PageSize : null,

                    skip:
                        (pagination.Page > 0) ?
                            (pagination.PageSize > 0) ?
                            (pagination.Page - 1) * pagination.PageSize :
                        (pagination.Page - 1) * 10 : null // Default pageSize = 10
                );


            var variants = productVariants.Select(pv => new ProductVariantDto
            {
                Id = pv.Id.ToString(),
                Attribute = pv.Variant.Attribute.Name,
                AttributeType = pv.Variant.Attribute.Type.ToString(),
                Value = pv.Variant.Value,
                Images = pv.VariantImages.Select(vi => new ProductImageDto
                {
                    Path = vi.Image.Path,
                }).ToList()
            });

            return new PaginationOutputDto<ProductVariantDto>(variants.ToList(), totalCount, page, pageSize);
        }

        private static Expression<Func<ProductVariant, object>> MapSortColumnToVariantProperty(string input)
        {
            switch (input.ToLower())
            {
                case "attribute":
                    return pv => pv.Variant.Attribute.Name;
                case "id":
                    return pv => pv.Id;
                default:
                    return pv => pv.CreatedAt;
            }
        }

        public async Task<ProductDto> GetParentAsync(Guid parentProductId)
        {
            var product = await _uow.ProductRepository.GetByIdAsync(parentProductId);

            if (product == null)
                return null; // No product with this id

            // manual mapping (product exist)
            var productDto = new ProductDto
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Desc = product.Desc,
                MainImagePath = product?.Images?.FirstOrDefault(i => i.IsMain)?.Path ?? "",
                MinorImages = product.Images.Where(img => !img.IsMain).Select(img => new ProductImageDto
                {
                    Path = img.Path
                }),
                Translations = product.ProductLanguages.Select(language => new ProductLanguageDto
                {
                    Name = language.Name,
                    Desc = language.Desc,
                    LanguageCode = language.languageCode

                })
            };

            return productDto;

        }

        public async Task<ProductVariantDto> GetVariantAsync(Guid id)
        {
            var productVariant = await _uow.ProductVariantRepository.GetByIdAsync(id);

            if (productVariant == null)
                return null; // No product with this id

            return new ProductVariantDto
            {
                Id = productVariant.Id.ToString(),
                Attribute = productVariant.Variant.Attribute.Name,
                AttributeType = productVariant.Variant.Attribute.Type.ToString(),
                ParentProductId = productVariant.Product.Id.ToString(),
                Images = productVariant.VariantImages.Select(vi => new ProductImageDto
                {
                    Path = vi.Image.Path,
                }).ToList()
            };
        }

        public async Task RemoveVariantAsync(Guid id)
        {
            var productVariant = await _uow.ProductVariantRepository.GetByIdAsync(id);
            var VariantImages = await _uow.VariantImageRepository.FindAsync(vi => vi.ProductVariant.Id == id);
            await _uow.VariantImageRepository.RemoveRangeAsync(VariantImages);
            await _uow.ProductVariantRepository.RemoveAsync(productVariant);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateVariantAsync(ProductVariantDto variant)
        {
            var dbProductVariant = await _uow.ProductVariantRepository
                    .FirstOrDefaultAsync(pv => pv.Id.ToString() == variant.Id);

            dbProductVariant.Variant.Value = variant.Value;
            dbProductVariant.Variant.Attribute.Name = variant.Attribute;
            dbProductVariant.Variant.Attribute.Type = HelperMethod.MapAttributeTypeToEnum(variant.AttributeType);

            IList<VariantImage> newVaraintImages = new List<VariantImage>();
            foreach (var image in variant.Images)
            {
                var dbImag = dbProductVariant.Product.Images.FirstOrDefault(img => img.Path == image.Path);
                if (dbImag != null)
                    newVaraintImages.Add(new VariantImage
                    {
                        Image = dbImag,
                        ProductVariant = dbProductVariant,
                    });
            }
            dbProductVariant.VariantImages = newVaraintImages;

            await _uow.ProductVariantRepository.UpdateAsync(dbProductVariant);
            await _uow.SaveChangesAsync();
        }
    }
}
