using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System;

public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Inicio");
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
        consumer.Received += (sender, eventArgs) =>
        {
            #region implementação pessoal
            // 1. Obter o array de bytes da mensagem:
            var contentArray = eventArgs.Body.ToArray();

            // 2. Converter o array de bytes para uma string UTF-8:
            var contentString = Encoding.UTF8.GetString(contentArray);

            // 3. Desserializar a string JSON em um objeto do tipo 'Pessoa':
            var mensagem = JsonSerializer.Deserialize<Pessoa>(contentString);

            Console.WriteLine($"Mensagem recebida: {contentString}");

            // 4. Confirmar o recebimento da mensagem (ACK):
            canalConsumer.BasicAck(eventArgs.DeliveryTag,false);
            #endregion
        };

        canalConsumer.BasicConsume(QUEUE,false, consumer);


        Thread.Sleep(TimeSpan.FromSeconds(1));
    }

    public class Pessoa
    {
        public Pessoa(int id, string? nome, string? sobrenome)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
        }

        public int Id { get; set; }
        public string?  Nome { get; set; }
        public string? Sobrenome { get; set; }

    }
}