using _123Vendas.Vendas.Application.DTOs;
using _123Vendas.Vendas.Infrastructure.RabbitMQ.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _123Vendas.Vendas.Infrastructure.RabbitMQ.Consumers
{
    public class VendasMessageConsumerService : BaseMessageConsumerService<ProdutoDTO>
    {
        public VendasMessageConsumerService(IOptions<RabbitMQConfiguracao> rabbitMQConfiguracao, ILogger<ProdutoDTO> logger) : base(rabbitMQConfiguracao, logger)
        {
        }
    }
}
