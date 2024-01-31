using Domain.Entities;
using Domain.Enums.EntitiesEnums;
using Microsoft.EntityFrameworkCore;

using Attribute = Domain.Entities.Attribute;

namespace Infrastructure.Persistence
{
    public static class DataSeeder
    {
        //private static readonly Random _random = new Random();

        public static async Task SeedDataAsync(AppDbContext dbContext)
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await dbContext.Database.MigrateAsync();
            }


            await SeedAttributesAsync(dbContext);
            await SeedVariantsAsync(dbContext);
            await SeedProductsAsync(dbContext);
            await SeedProductLanguageAsync(dbContext);
            await SeedProductsVariantsAsync(dbContext);
            await SeedImagesAsync(dbContext);
            await SeedVariantImagesAsync(dbContext);

        }

        private static async Task SeedImagesAsync(AppDbContext dbContext)
        {
            if (!dbContext.Images.Any())
            {
                var images = new List<Image>
                {
                    new Image { Product = dbContext.Products.First(p => p.Name == "T-Shirt"), IsMain = true, Path = @"C:\image\path.jpg" },
                    new Image { Product = dbContext.Products.First(p => p.Name == "T-Shirt"), Path = @"C:\image\path2.jpg" },
                    new Image { Product = dbContext.Products.First(p => p.Name == "T-Shirt"), Path = @"C:\image\path3.jpg" },
                    new Image { Product = dbContext.Products.First(p => p.Name == "T-Shirt"), Path = @"C:\image\path4.jpg" }
                };
                await dbContext.Images.AddRangeAsync(images);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedVariantImagesAsync(AppDbContext dbContext)
        {
            if (!dbContext.VariantImages.Any())
            {
                var variantImages = new List<VariantImage>
                {
                    new VariantImage { ProductVariant = dbContext.ProductVariants.First(pv => pv.Product.Name == "T-Shirt" && pv.Variant.Value == "Green"), Image = dbContext.Images.First(i => i.Path == @"C:\image\path2.jpg") },
                    new VariantImage { ProductVariant = dbContext.ProductVariants.First(pv => pv.Product.Name == "T-Shirt" && pv.Variant.Value == "Green"), Image = dbContext.Images.First(i => i.Path == @"C:\image\path3.jpg") },
                };
                await dbContext.VariantImages.AddRangeAsync(variantImages);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedProductsVariantsAsync(AppDbContext dbContext)
        {
            if (!dbContext.ProductVariants.Any())
            {
                var productVariants = new List<ProductVariant>
                {
                    new ProductVariant { Product = dbContext.Products.First(p => p.Name == "T-Shirt"), Variant = dbContext.Variants.First(v => v.Value == "Red")},
                    new ProductVariant { Product = dbContext.Products.First(p => p.Name == "T-Shirt"), Variant = dbContext.Variants.First(v => v.Value == "Green")},
                    new ProductVariant { Product = dbContext.Products.First(p => p.Name == "T-Shirt"), Variant = dbContext.Variants.First(v => v.Value == "l")},
                    new ProductVariant { Product = dbContext.Products.First(p => p.Name == "T-Shirt"), Variant = dbContext.Variants.First(v => v.Value == "60% cotton, 40% polyester")},
                    new ProductVariant { Product = dbContext.Products.First(p => p.Name == "Battary"), Variant = dbContext.Variants.First(v => v.Value == "10AH")},
                    new ProductVariant { Product = dbContext.Products.First(p => p.Name == "Battary"), Variant = dbContext.Variants.First(v => v.Value == "9800mAH")},
                    new ProductVariant { Product = dbContext.Products.First(p => p.Name == "Hat"), Variant = dbContext.Variants.First(v => v.Value == "Blue")},
                    new ProductVariant { Product = dbContext.Products.First(p => p.Name == "T-Shirt2"), Variant = dbContext.Variants.First(v => v.Value == "s")},
                    new ProductVariant { Product = dbContext.Products.First(p => p.Name == "T-Shirt2"), Variant = dbContext.Variants.First(v => v.Value == "m")},
                    new ProductVariant { Product = dbContext.Products.First(p => p.Name == "T-Shirt2"), Variant = dbContext.Variants.First(v => v.Value == "Blue")},
                    new ProductVariant { Product = dbContext.Products.First(p => p.Name == "T-Shirt2"), Variant = dbContext.Variants.First(v => v.Value == "100% cotton")},
                };
                await dbContext.ProductVariants.AddRangeAsync(productVariants);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedProductLanguageAsync(AppDbContext dbContext)
        {
            if (!dbContext.ProductLanguages.Any())
            {
                var productLanguages = new List<ProductLanguage>
                {
                    new ProductLanguage {languageCode = "ar", Name = "قميص", Desc = "قميص صيفي جميل", Product = dbContext.Products.First(p => p.Name == "T-Shirt")},
                    new ProductLanguage {languageCode = "tr", Name = "gömlek", Desc = "Çok güzel yazlık bir gömlek", Product = dbContext.Products.First(p => p.Name == "T-Shirt")},
                    new ProductLanguage {languageCode = "ar", Name = "قبعة", Desc = "قبعة صيفية جميلة", Product = dbContext.Products.First(p => p.Name == "Hat")},
                    new ProductLanguage {languageCode = "ar", Name = "بطارية", Desc = "بطارية كبيرة لكل حالات الاستخدام", Product = dbContext.Products.First(p => p.Name == "Battary")},
                };
                await dbContext.ProductLanguages.AddRangeAsync(productLanguages);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedProductsAsync(AppDbContext dbContext)
        {
            if (!dbContext.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product { Name = "T-Shirt", Desc = "Nice summer T-Shirt!"},
                    new Product { Name = "T-Shirt2", Desc = "Nice summer T-Shirt2!"},
                    new Product { Name = "Hat", Desc = "Nice summer Hat!"},
                    new Product { Name = "Battary", Desc = "Big battary for all use cases!"},
                };
                await dbContext.Products.AddRangeAsync(products);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedVariantsAsync(AppDbContext dbContext)
        {
            if (!dbContext.Variants.Any())
            {
                var variants = new List<Variant>
                {
                    new Variant { Attribute = dbContext.Attributes.First(a => a.Name == "Color"), Value = "Red"},
                    new Variant { Attribute = dbContext.Attributes.First(a => a.Name == "Color"), Value = "Green"},
                    new Variant { Attribute = dbContext.Attributes.First(a => a.Name == "Color"), Value = "Blue"},

                    new Variant { Attribute = dbContext.Attributes.First(a => a.Name == "Size"), Value = "S"},
                    new Variant { Attribute = dbContext.Attributes.First(a => a.Name == "Size"), Value = "M"},
                    new Variant { Attribute = dbContext.Attributes.First(a => a.Name == "Size"), Value = "L"},

                    new Variant { Attribute = dbContext.Attributes.First(a => a.Name == "Material"), Value = "60% cotton, 40% polyester"},
                    new Variant { Attribute = dbContext.Attributes.First(a => a.Name == "Material"), Value = "100% cotton"},

                    new Variant { Attribute = dbContext.Attributes.First(a => a.Name == "Capacity"), Value = "10AH"},
                    new Variant { Attribute = dbContext.Attributes.First(a => a.Name == "Capacity"), Value = "9800mAH"},
                };
                await dbContext.Variants.AddRangeAsync(variants);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedAttributesAsync(AppDbContext dbContext)
        {
            if (!dbContext.Attributes.Any())
            {
                var attributes = new List<Attribute>
                {
                    new Attribute { Name = "Color", Type = AttributeTypeEnum.Text},
                    new Attribute { Name = "Size", Type = AttributeTypeEnum.Select},
                    new Attribute { Name = "Material", Type = AttributeTypeEnum.Text},
                    new Attribute { Name = "Capacity", Type = AttributeTypeEnum.Text}
                };
                await dbContext.Attributes.AddRangeAsync(attributes);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
