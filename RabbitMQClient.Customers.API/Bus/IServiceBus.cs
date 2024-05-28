namespace RabbitMQClient.Customers.API.Bus
{
    public interface IServiceBus
    {
        void Publish<T>(string routingKey, T message);
    }
}
