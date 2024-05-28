using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQClient.Customers.API.Bus
{
    public class RabbitMqClientService : IServiceBus
    {
        private readonly IModel _chanel;
        const string EXCHANGE = "curso-rabbitmq";


        public RabbitMqClientService()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var conncetion = connectionFactory.CreateConnection("conexao-curso-rabbitmq");
            _chanel = conncetion.CreateModel();

        }
        public void Publish<T>(string routingKey, T message)
        {
            var json = JsonSerializer.Serialize(message);
            var byteArray = Encoding.UTF8.GetBytes(json);

            _chanel.BasicPublish(EXCHANGE, routingKey, null, byteArray);
        }
    }
}
