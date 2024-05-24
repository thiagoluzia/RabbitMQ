using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

public class Program
{
    private static void Main(string[] args)
    {
        //Criar uma fila
        const string QUEUE = "praticando-rabbit-mq-fila";

        //Construir uma conexão com o servidor
        var connectionFactory = new ConnectionFactory
        {
            HostName = @$"localhost"
        };

        //Criar uma conexão com o RabbitMQ
        var conexaoConsumer = connectionFactory.CreateConnection("curso-rabbitmq");

        //Criar um canal por meio da conexão criada
        var canalConsumer = conexaoConsumer.CreateModel();

        //criar um consumer
        var consumer = new EventingBasicConsumer(canalConsumer);

        //Consumidor recebe e processa uma mensagem recebida
        consumer.Received += async (sender, eventArgs) =>
        {
            // 1. Obter o array de bytes da mensagem:
            var contentArray = eventArgs.Body.ToArray();

            // 2. Converter o array de bytes para uma string UTF-8:
            var contentString = Encoding.UTF8.GetString(contentArray);

            // 3. Desserializar a string JSON em um objeto do tipo 'Pessoa':
            var mensagem = JsonSerializer.Deserialize<Pessoa>(contentString);

            Console.WriteLine($"Mensagem recebida: {contentString}");

            // 4. Confirmar o recebimento da mensagem (ACK):
            canalConsumer.BasicAck(eventArgs.DeliveryTag, false);
            
        };

        canalConsumer.BasicConsume(QUEUE, false, consumer);

    }

    public class Pessoa
    {
        public int Id { get; set; }
        public string?  Nome { get; set; }
        public string? Sobrenome { get; set; }

    }
}