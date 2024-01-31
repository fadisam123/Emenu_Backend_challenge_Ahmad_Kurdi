using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EmenuDB_Ahmad_kurdi;Trusted_Connection=True;"))
                .BuildServiceProvider();

            return serviceProvider.GetRequiredService<AppDbContext>();
        }
    }
}
