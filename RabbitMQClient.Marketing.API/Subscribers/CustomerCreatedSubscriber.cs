using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQClient.Marketing.API.DTO;
using System.Text;
using System.Text.Json;

namespace RabbitMQClient.Marketing.API.Subscribers
{
    public class CustomerCreatedSubscriber : IHostedService
    {
        private readonly IModel _chanel;

        const string EXCHANGE = "curso-rabbitmq";
        const string CUSTOMER_CREATED_QUEUE = "customer-created";

        public CustomerCreatedSubscriber()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = connectionFactory.CreateConnection("NomeConexao");

            _chanel = connection.CreateModel();
           
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_chanel);

            consumer.Received += (sender, e) =>
            {
                var contentArray = e.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var @event = JsonSerializer.Deserialize<CustomerCreated>(contentString);

                Console.WriteLine($"Mensagem recebida: {contentString}");

                _chanel.BasicAck(e.DeliveryTag, false);
            };

            _chanel.BasicConsume(CUSTOMER_CREATED_QUEUE, false, consumer);
        }

        public  Task StopAsync(CancellationToken cancellationToken)
        {
            return  Task.CompletedTask;
        }
    }
}
