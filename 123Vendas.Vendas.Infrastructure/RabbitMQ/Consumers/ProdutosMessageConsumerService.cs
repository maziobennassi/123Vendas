using _123Vendas.Vendas.Application.DTOs;
using _123Vendas.Vendas.Infrastructure.RabbitMQ.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace _123Vendas.Vendas.Infrastructure.RabbitMQ.Consumers
{
    public class ProdutosMessageConsumerService : BaseMessageConsumerService<ProdutoDTO>
    {
        public ProdutosMessageConsumerService(IOptions<RabbitMQConfiguracao> rabbitMQConfiguracao, ILogger<ProdutoDTO> logger) : base(rabbitMQConfiguracao, logger)
        {
        }
    }
}
