using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using _123Vendas.Vendas.Infrastructure.RabbitMQ.Settings;
using Microsoft.Extensions.Logging;

namespace _123Vendas.Vendas.Infrastructure.RabbitMQ.Consumers
{
    public class BaseMessageConsumerService<T> : BackgroundService
    {
        private readonly RabbitMQConfiguracao _rabbitMQConfiguracao;
        private IConnection _conexao;
        private IModel _canal;
        private readonly ILogger<T> _logger;

        public BaseMessageConsumerService(IOptions<RabbitMQConfiguracao> rabbitMQConfiguracao, ILogger<T> logger)
        {
            _logger = logger;
            _rabbitMQConfiguracao = rabbitMQConfiguracao.Value;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQConfiguracao.HostName,
                UserName = _rabbitMQConfiguracao.UserName,
                Password = _rabbitMQConfiguracao.Password
            };
            _conexao = factory.CreateConnection();
            _canal = _conexao.CreateModel();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StartConsuming(RabbitMQFilas.VendasQueue, stoppingToken);
            await Task.CompletedTask;
        }

        private void StartConsuming(string queueName, CancellationToken cancellationToken)
        {
            _canal.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_canal);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(body);

                bool processadoComSucesso = false;
                try
                {
                    processadoComSucesso = await ProcessMessageAsync(mensagem);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Algum erro ocorreu durante o processamento da mensagem da fila {queueName}: {ex}");
                }

                if (processadoComSucesso)
                {
                    _canal.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                else
                {
                    _canal.BasicReject(deliveryTag: ea.DeliveryTag, requeue: true);
                }
            };

            _canal.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

        private async Task<bool> ProcessMessageAsync(string message)
        {
            try
            {
                var objeto = JsonConvert.DeserializeObject<T>(message);

                if (objeto.GetType().GetProperty("cancelado").GetValue(objeto, null).Equals(true))
                {
                    _logger.LogInformation("Mensagem de operação de cancelamento", JsonConvert.SerializeObject(objeto));
                    return true;
                }

                _logger.LogInformation("Mensagem de operação de Inserção/Atualização de venda", JsonConvert.SerializeObject(objeto));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro durante o processamento da mensagem: {ex.Message}");
                return false;
            }
        }

        public override void Dispose()
        {
            _canal.Close();
            _conexao.Close();
            base.Dispose();
        }
    }
}
