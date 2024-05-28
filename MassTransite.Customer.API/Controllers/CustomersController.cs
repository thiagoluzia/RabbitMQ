using MassTransite.Customer.API.Bus;
using MassTransite.Customer.API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MassTransite.Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IServiceBus _bus;

        public CustomersController(IServiceBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerInputModel model)
        {
            var @event = new CreateCustomerInputModel(model.Id, model.Name, model.Document,model.Email);

            _bus.Publish(@event);

            return NoContent();
        }
    }
}
