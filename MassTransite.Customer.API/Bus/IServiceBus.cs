namespace MassTransite.Customer.API.Bus
{
    public interface IServiceBus
    {
        Task Publish<T>(T message); 
    }
}
