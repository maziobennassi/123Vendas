using _123Vendas.Vendas.Application.Interfaces.Publishers;
using _123Vendas.Vendas.Infrastructure.RabbitMQ.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace _123Vendas.Vendas.Infrastructure.RabbitMQ.Publishers
{
    public class RabbitMQPublisher<T> : IRabbitMQPublisher<T>
    {
        private readonly RabbitMQConfiguracao _rabbitMQConfiguracoes;

        public RabbitMQPublisher(IOptions<RabbitMQConfiguracao> rabbitMQConfiguracoes)
        {
            _rabbitMQConfiguracoes = rabbitMQConfiguracoes.Value;
        }

        public async Task PublicarMensagemAsync(T mensagem, string nomeFila)
        {

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQConfiguracoes.HostName,
                UserName = _rabbitMQConfiguracoes.UserName,
                Password = _rabbitMQConfiguracoes.Password
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: nomeFila, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var messageJson = JsonConvert.SerializeObject(mensagem);
            var body = Encoding.UTF8.GetBytes(messageJson);

            await Task.Run(() => channel.BasicPublish(exchange: "", routingKey: nomeFila, basicProperties: null, body: body));
        }
    }
}
