namespace Infrastructure.Persistence.Repository
{
    public class AttributeRepository : GenericRepository<Domain.Entities.Attribute>, IAttributeRepository
    {
        public AttributeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
