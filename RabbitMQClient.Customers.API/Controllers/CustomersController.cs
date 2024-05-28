using Microsoft.AspNetCore.Mvc;
using RabbitMQClient.Customers.API.Bus;
using RabbitMQClient.Customers.API.DTO;

namespace RabbitMQClient.Customers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        const string ROUTING_KEY = "customer-created";
        private readonly IServiceBus _bus;

        public CustomersController(IServiceBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public IActionResult Post(CustomerImputModel model)
        {
            var @evento = new CustomerImputModel(model.Id, model.Name, model.Document, model.Email);
            _bus.Publish(ROUTING_KEY, @evento);

            return NoContent();
        }
    }
}
