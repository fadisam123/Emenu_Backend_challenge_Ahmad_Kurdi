namespace Infrastructure.Persistence.Repository
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
