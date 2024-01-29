using Application.Persistence.Repository;

namespace Application.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IAttributeRepository AttributeRepository { get; }
        public IImageRepository ImageRepository { get; }
        public IProductLanguageRepository ProductLanguageRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductVariantRepository ProductVariantRepository { get; }
        public IVariantImageRepository VariantImageRepository { get; }
        public IVariantRepository VariantRepository { get; }
        public Task SaveChangesAsync();
    }
}
