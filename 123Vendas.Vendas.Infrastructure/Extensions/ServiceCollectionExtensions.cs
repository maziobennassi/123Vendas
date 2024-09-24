using _123Vendas.Vendas.Application.Interfaces.Publishers;
using _123Vendas.Vendas.Application.Interfaces.Services;
using _123Vendas.Vendas.Application.Services;
using _123Vendas.Vendas.Application.Services.Interfaces;
using _123Vendas.Vendas.Domain.Repositories;
using _123Vendas.Vendas.Infrastructure.Mappers.Profiles;
using _123Vendas.Vendas.Infrastructure.Persistance;
using _123Vendas.Vendas.Infrastructure.RabbitMQ.Consumers;
using _123Vendas.Vendas.Infrastructure.RabbitMQ.Publishers;
using _123Vendas.Vendas.Infrastructure.RabbitMQ.Settings;
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

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }

        public static void AddServicesAndRepositories(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IVendasRepository, VendasRepository>();

            //Services
            services.AddScoped<IVendasService, VendasService>();

            //RabbitMQ
            services.AddHostedService<VendasMessageConsumerService>();
            services.AddHostedService<ProdutosMessageConsumerService>();
            services.AddScoped(typeof(IRabbitMQPublisher<>), typeof(RabbitMQPublisher<>));
        }
    }
}
