using MassTransit;
using MassTransite.Marketing.API.DTO;
using MassTransite.Marketing.API.Services;

namespace MassTransite.Marketing.API.Subscribers
{
    public class CustpmerCreatedSubscriber : IConsumer<CustomerViewModel>
    {
        public IServiceProvider ServiceProvider { get; }

        public CustpmerCreatedSubscriber(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        public async Task Consume(ConsumeContext<CustomerViewModel> context)
        {
            var @event = context.Message;

            using (var scope = ServiceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<INotification>();

                await service.Text(@event.Name);
            }
        }

        
    }
}
