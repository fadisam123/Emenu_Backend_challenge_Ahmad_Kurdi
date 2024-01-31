using Application.Persistence.UnitOfWork;

namespace Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _dbContext;

        public UnitOfWork(
            AppDbContext context,
            IAttributeRepository AttributeRepository,
            IImageRepository ImageRepository,
            IProductLanguageRepository ProductLanguageRepository,
            IProductRepository ProductRepository,
            IProductVariantRepository ProductVariantRepository,
            IVariantImageRepository VariantImageRepository,
            IVariantRepository VariantRepository
            )
        {
            _dbContext = context;
            this.AttributeRepository = AttributeRepository;
            this.ImageRepository = ImageRepository;
            this.ProductLanguageRepository = ProductLanguageRepository;
            this.AttributeRepository = AttributeRepository;
            this.ProductRepository = ProductRepository;
            this.ProductVariantRepository = ProductVariantRepository;
            this.VariantImageRepository = VariantImageRepository;
            this.VariantRepository = VariantRepository;
        }

        public IAttributeRepository AttributeRepository { get; }
        public IImageRepository ImageRepository { get; }
        public IProductLanguageRepository ProductLanguageRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductVariantRepository ProductVariantRepository { get; }
        public IVariantImageRepository VariantImageRepository { get; }
        public IVariantRepository VariantRepository { get; }


        private bool _disposed;
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await _dbContext.DisposeAsync();
                }

                _disposed = true;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
