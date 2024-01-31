using Application.Persistence.UnitOfWork;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static async Task<IServiceCollection> AddInfrastructureServicesAsync(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDbContextPool<AppDbContext>(option =>
                    option.UseSqlServer(configuration.GetConnectionString("DevelopmentConnection"),
                    SqlOption => SqlOption.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                          .UseLazyLoadingProxies());

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IProductLanguageRepository, ProductLanguageRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
            services.AddScoped<IVariantImageRepository, VariantImageRepository>();
            services.AddScoped<IVariantRepository, VariantRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            await SeedDataInDbAsync(services.BuildServiceProvider());
            return services;
        }

        private static async Task SeedDataInDbAsync(IServiceProvider services)
        {
            using (IServiceScope serviceScope = services.CreateScope())
            {
                IServiceProvider serviceProvider = serviceScope.ServiceProvider;
                var DbContext = serviceProvider.GetRequiredService<AppDbContext>();
                await DataSeeder.SeedDataAsync(DbContext);
            }
        }
    }
}
