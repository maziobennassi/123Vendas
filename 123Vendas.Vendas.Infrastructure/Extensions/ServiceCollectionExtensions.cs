using _123Vendas.Vendas.Application.Interfaces.Publishers;
using _123Vendas.Vendas.Application.Interfaces.Services;
using _123Vendas.Vendas.Application.Services;
using _123Vendas.Vendas.Application.Services.Interfaces;
using _123Vendas.Vendas.Domain.Repositories;
using _123Vendas.Vendas.Infrastructure.Mappers.Profiles;
using _123Vendas.Vendas.Infrastructure.Persistance;
using _123Vendas.Vendas.Infrastructure.RabbitMQ.Consumers;
using _123Vendas.Vendas.Infrastructure.RabbitMQ.Publishers;
using _123Vendas.Vendas.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;

namespace _123Vendas.Vendas.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, Boolean isDevelopment)
        {
            var connectionString = configuration.GetConnectionString("VendasDb");

            services.AddDbContext<VendasDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void AddMigrations(this IServiceProvider services)
        {
            var db = services.GetRequiredService<VendasDbContext>();
            db.Database.Migrate();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }

        public static void AddServicesAndRepositories(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IVendasRepository, VendasRepository>();
            services.AddScoped<IProdutosRepository, ProdutosRepository>();

            //Services
            services.AddScoped<IVendasService, VendasService>();
            services.AddScoped<IProdutosService, ProdutosService>();

            //RabbitMQ
            services.AddHostedService<VendasMessageConsumerService>();
            services.AddHostedService<ProdutosMessageConsumerService>();
            services.AddScoped(typeof(IRabbitMQPublisher<>), typeof(RabbitMQPublisher<>));
        }
    }
}
