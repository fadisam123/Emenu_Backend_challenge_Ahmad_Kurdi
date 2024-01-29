using Application.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
