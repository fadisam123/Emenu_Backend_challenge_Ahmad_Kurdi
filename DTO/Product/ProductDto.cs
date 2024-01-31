namespace DTOs.Product
{
    // For simplicity, I will use this DTOs for input and output operations (but it is better to separate them).
    // Also, it is better to isolate each DTO class in a separate file.
    // for example most input DTOs do not need Id prop
    public class ProductDto
    {
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Desc { get; set; }
        public string? MainImagePath { get; set; }
        public IEnumerable<ProductImageDto> MinorImages { get; set; } = new List<ProductImageDto>();
        public IEnumerable<ProductLanguageDto> Translations { get; set; } = new List<ProductLanguageDto>();
        public IEnumerable<ProductVariantDto> Variants { get; set; } = new List<ProductVariantDto>();
    }

    public class ProductLanguageDto
    {
        public string Name { get; set; } = null!;
        public string? Desc { get; set; }
        public string LanguageCode { get; set; } = null!;

    }

    public class ProductVariantDto
    {
        public string? Id { get; set; }
        public string? ParentProductId { get; set; }
        public string Attribute { get; set; } = null!;
        public string AttributeType { get; set; } = null!;
        public string Value { get; set; } = null!;
        public IEnumerable<ProductImageDto> Images { get; set; } = new List<ProductImageDto>();
    }

    public class ProductImageDto
    {
        public string Path { get; set; } = null!;
    }
}
