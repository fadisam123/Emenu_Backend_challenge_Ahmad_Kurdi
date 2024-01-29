namespace Infrastructure.Persistence.Repository
{
    public class VariantRepository : GenericRepository<Variant>, IVariantRepository
    {
        public VariantRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
