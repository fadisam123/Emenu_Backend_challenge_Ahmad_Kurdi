namespace Infrastructure.Persistence.Repository
{
    public class ProductVariantRepository : GenericRepository<ProductVariant>, IProductVariantRepository
    {
        public ProductVariantRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
