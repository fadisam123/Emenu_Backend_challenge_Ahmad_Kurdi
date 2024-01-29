namespace Infrastructure.Persistence.Repository
{
    public class ProductLanguageRepository : GenericRepository<ProductLanguage>, IProductLanguageRepository
    {
        public ProductLanguageRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
