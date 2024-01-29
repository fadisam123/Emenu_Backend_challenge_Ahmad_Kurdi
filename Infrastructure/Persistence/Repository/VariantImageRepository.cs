namespace Infrastructure.Persistence.Repository
{
    public class VariantImageRepository : GenericRepository<VariantImage>, IVariantImageRepository
    {
        public VariantImageRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
