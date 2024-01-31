namespace Application.Services.Product
{
    using Application.Services;
    using Domain.Entities;
    using Domain.Enums.EntitiesEnums;
    using DTOs.Product;
    using System;
    using System.Linq.Expressions;

    public class ProductService : IProductService
    {
        private IUnitOfWork _uow { get; }
        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        private static Expression<Func<Product, object>> MapSortColumnToProductProperty(string input)
        {
            switch (input.ToLower())
            {
                case "name":
                    return p => p.Name;
                case "desc":
                    return p => p.Desc;
                case "id":
                    return p => p.Id;
                default:
                    return p => p.CreatedAt;
            }
        }
        public async Task<PaginationOutputDto<ProductDto>> GetAllProductsAsync(PaginationInputDto pagination)
        {
            int totalCount = await _uow.ProductRepository.CountAsync();
            int page = pagination?.Page ?? 1;
            int? pageSize = pagination?.PageSize ?? null;

            IEnumerable<Product> products = await _uow.ProductRepository
                .GetAllAsync
                (
                    // if filter is send then apply
                    // filtering by product [name || description (including other languages)] ||
                    // its variants attributes value (e.g., red, small, etc)
                    filter:
                        (!String.IsNullOrEmpty(pagination.Filter)) ?
                        (
                            p => p.Name.ToLower().Contains(pagination.Filter) ||
                            ((p.Desc != null) && p.Desc.ToLower().Contains(pagination.Filter)) ||
                            p.ProductLanguages
                            .Any(pl =>
                                pl.Name.ToLower().Contains(pagination.Filter) ||
                                ((pl.Desc != null) && pl.Desc.ToLower().Contains(pagination.Filter))) ||
                            p.ProductVariants.Any(pv => pv.Variant.Value.ToLower().Contains(pagination.Filter))

                        ) : null,

                    // if sort is send then apply
                    // sorting on the product property the user has specified
                    orderBy:
                        (!String.IsNullOrEmpty(pagination.SortColumn)) ?
                        (
                            // if user specify [asc] order
                            p => (pagination.SortOrder.ToLower() == "asc") ?
                            p.OrderBy(MapSortColumnToProductProperty(pagination.SortColumn)) :
                            // else user specify [desc] order (Also, the default order is desc if the user does not specify)
                            p.OrderByDescending(MapSortColumnToProductProperty(pagination.SortColumn))
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

            // manual mapping
            var productsDto = products.Select(product => new ProductDto
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

                }),
                Variants = product.ProductVariants.Select(pv => new ProductVariantDto
                {
                    Id = pv.Id.ToString(),
                    Attribute = pv.Variant.Attribute.Name,
                    AttributeType = pv.Variant.Attribute.Type.ToString(),
                    Value = pv.Variant.Value,
                    Images = pv.VariantImages.ToList().Select(vi => new ProductImageDto
                    {
                        Path = vi.Image.Path
                    })
                })
            });

            return new PaginationOutputDto<ProductDto>(productsDto.ToList(), totalCount, page, pageSize);
        }

        public async Task<ProductDto> GetProductAsync(Guid id)
        {
            var product = await _uow.ProductRepository.GetByIdAsync(id);

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

                }),
                Variants = product.ProductVariants.Select(pv => new ProductVariantDto
                {
                    Id = pv.Id.ToString(),
                    Attribute = pv.Variant.Attribute.Name,
                    AttributeType = pv.Variant.Attribute.Type.ToString(),
                    Value = pv.Variant.Value,
                    Images = pv.VariantImages.ToList().Select(vi => new ProductImageDto
                    {
                        Path = vi.Image.Path
                    })
                })
            };

            return productDto;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto product)
        {
            // images of the product
            var images = product.MinorImages.Select(img => new Image
            {
                Path = img.Path,
            }).ToList();

            // main image
            if (!string.IsNullOrEmpty(product.MainImagePath))
                images.Add(new Image
                {
                    Path = product.MainImagePath,
                    IsMain = true,
                });

            // processing the variants of the product.
            // For each variant, if there is a pre-existing row with the
            // same details (i.e., same attribute name & value), we will link with it.
            // otherwise we will create a new one
            List<Variant> variants = new List<Variant>();
            foreach (var productVariant in product.Variants)
            {
                var variant = await _uow.VariantRepository
                    .FirstOrDefaultAsync
                    (
                        v =>
                        // attribute value (or variant) equality
                        v.Value.ToLower() == productVariant.Value.ToLower()
                        // attribute name equality
                        && v.Attribute.Name.ToLower() == productVariant.Attribute.ToLower()
                    );

                if (variant != null)
                    variants.Add(variant);
                else // the variant doesn't exist, create new one
                {
                    AttributeTypeEnum attributeType = HelperMethod.MapAttributeTypeToEnum(productVariant.AttributeType);
                    var newAttribute = await _uow.AttributeRepository.AddAsync(new Domain.Entities.Attribute
                    {
                        Name = productVariant.Attribute,
                        Type = attributeType
                    });
                    var newVariant = await _uow.VariantRepository.AddAsync(new Variant
                    {
                        Attribute = newAttribute,
                        Value = productVariant.Value
                    });
                    variants.Add(newVariant);
                }
            }

            var newProduct = new Product
            {
                Name = product.Name,
                Desc = product.Desc,
                Images = images,
                ProductLanguages = product.Translations.Select(language => new ProductLanguage
                {
                    Name = language.Name,
                    Desc = language.Desc,
                    languageCode = language.LanguageCode
                }).ToList(),
            };

            var createdProduct = await _uow.ProductRepository.AddAsync(newProduct);

            var productVariants = new List<ProductVariant>();
            productVariants.AddRange
                (
                    variants.Select(variant => new ProductVariant { Product = createdProduct, Variant = variant })
                );

            await _uow.ProductVariantRepository.AddRangeAsync(productVariants);

            foreach (var productVariant in product.Variants)
            {
                foreach (var image in productVariant.Images)
                {
                    var dbImag = createdProduct.Images.First(img => img.Path == image.Path);
                    var dbProductVariat = createdProduct.ProductVariants.First(pv => pv.Variant.Value.ToLower() == productVariant.Value.ToLower() && pv.Variant.Attribute.Name.ToLower() == productVariant.Attribute.ToLower());
                    await _uow.VariantImageRepository.AddAsync(new VariantImage
                    {
                        Image = dbImag,
                        ProductVariant = dbProductVariat,
                    });
                }
            }

            await _uow.SaveChangesAsync();
            return await GetProductAsync(createdProduct.Id);
        }

        public async Task UpdateProductAsync(ProductDto product)
        {
            // images of the product
            var images = product.MinorImages.Select(img => new Image
            {
                Path = img.Path,
            }).ToList();

            // main image
            if (!string.IsNullOrEmpty(product.MainImagePath))
                images.Add(new Image
                {
                    Path = product.MainImagePath,
                    IsMain = true,
                });

            var dbProduct = await _uow.ProductRepository.GetByIdAsync(Guid.Parse(product.Id));
            dbProduct.Name = product.Name;
            dbProduct.Desc = product.Desc;
            dbProduct.ProductLanguages = product.Translations.Select(language => new ProductLanguage
            {
                Name = language.Name,
                Desc = language.Desc,
                languageCode = language.LanguageCode
            }).ToList();
            dbProduct.Images = images;

            foreach (var productVariant in product.Variants)
            {
                var dbProductVariant = dbProduct.ProductVariants
                    .FirstOrDefault(pv => pv.Id.ToString() == productVariant.Id);
                if (dbProductVariant != null)
                {
                    dbProductVariant.Variant.Value = productVariant.Value;
                    dbProductVariant.Variant.Attribute.Name = productVariant.Attribute;
                    dbProductVariant.Variant.Attribute.Type = HelperMethod.MapAttributeTypeToEnum(productVariant.AttributeType);

                    IList<VariantImage> newVaraintImages = new List<VariantImage>();
                    foreach (var image in productVariant.Images)
                    {
                        var dbImag = dbProduct.Images.First(img => img.Path == image.Path);
                        if (dbImag != null)
                            newVaraintImages.Add(new VariantImage
                            {
                                Image = dbImag,
                                ProductVariant = dbProductVariant,
                            });
                    }
                    dbProductVariant.VariantImages = newVaraintImages;
                }
            }

            await _uow.ProductRepository.UpdateAsync(dbProduct);
            await _uow.SaveChangesAsync();
        }

        public async Task RemoveProductAsync(Guid id)
        {
            var dbProduct = await _uow.ProductRepository.GetByIdAsync(id);
            await _uow.ProductRepository.RemoveAsync(dbProduct);
            await _uow.SaveChangesAsync();
        }
    }
}
