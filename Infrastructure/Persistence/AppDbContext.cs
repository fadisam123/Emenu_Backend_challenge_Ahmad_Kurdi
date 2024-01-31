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

            modelBuilder.Entity<ProductVariant>()
                .HasMany(pv => pv.VariantImages)
                .WithOne(vi => vi.ProductVariant)
                .OnDelete(DeleteBehavior.NoAction);
        }



    }
}
