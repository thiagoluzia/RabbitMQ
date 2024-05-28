using MassTransit;
using MassTransite.Customer.API.DTO;

namespace MassTransite.Customer.API.Bus
{
    public class MassTransitBusServce : IServiceBus
    {
        private readonly IBus _bus;

        public MassTransitBusServce(IBus bus)
        {
            _bus = bus;
        }

        public async Task Publish<T>(T message)
        {
            await _bus.Publish(message);

        }
    }
}
