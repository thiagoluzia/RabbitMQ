using RabbitMQ.Client;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

public  class Program
{
    private static void Main(string[] args)
    {
        //Criar a exchange
        const string  EXCHANGE = "praticando-rabbit-mq";

        //Criar a rout key 
        const string ROUTING_KEY = "praticando-rabbit-mq-rota";

        //Construir uma conexão com o servidor
        var connectionFactory = new ConnectionFactory
        {
            HostName = @$"localhost"// endereço do servidor 
        };

        //Criar uma conexão com o RabbitMQ
        var conxeao = connectionFactory.CreateConnection("DandoUmNomeParaConexaoAtual");

        //Criar um canal por meio da conexão criada
        var canal = conxeao.CreateModel();

        //serializar para Json o objeto a ser enviado
        var objetoJson = JsonSerializer.Serialize(new Pessoa { Id = 1, Nome = "Thiago", Sobrenome = "Moura" });

        //Converter o Json para um byte array
        var objetoByteArray = Encoding.UTF8.GetBytes(objetoJson);

        // Publicar pelo canal passando a (exchange, route key, duração da mensagem, e a menssagem
        canal.BasicPublish(EXCHANGE, ROUTING_KEY, null, objetoByteArray);

    }

    public class Pessoa()
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
    }
}