namespace MassTransite.Customer.API.Bus
{
    public interface IServiceBus
    {
        void Publish<T>(T message); 
    }
}
