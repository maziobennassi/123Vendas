using _123Vendas.Vendas.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _123Vendas.Vendas.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("VendasDb");
            services.AddDbContext<VendasDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
