using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.Attribute> Attributes { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductLanguage> ProductLanguages { get; set; } = null!;
        public DbSet<ProductVariant> ProductVariants { get; set; } = null!;
        public DbSet<Variant> Variants { get; set; } = null!;
        public DbSet<VariantImage> VariantImages { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Emenu");

            // Here I am configuring the foreign keys of Product/Image manually
            modelBuilder.Entity<Product>()
                .HasOne(p => p.MainImage)
                .WithOne(i => i.MainProduct)
                .HasForeignKey<Product>(p => p.MainImageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(i => i.AdditionalProduct)
                .HasForeignKey(i => i.AdditionalProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductVariant>()
                .HasMany(pv => pv.VariantImages)
                .WithOne(vi => vi.ProductVariant)
                .OnDelete(DeleteBehavior.NoAction);
        }



    }
}
