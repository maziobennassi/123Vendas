namespace _123Vendas.Vendas.Application.Interfaces.Publishers
{
    public interface IRabbitMQPublisher<T>
    {
        Task PublicarMensagemAsync(T mensagem, string nomeFila);
    }
}
