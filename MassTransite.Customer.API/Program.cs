using MassTransit;
using MassTransite.Customer.API.Bus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IServiceBus, MassTransitBusServce>();


builder.Services.AddMassTransit(c =>
{
    c.UsingRabbitMq((context, config) =>
    {
        config.ReceiveEndpoint("customer-created-masstransit", e =>
        {
            e.Bind("MassTransite.Customer.API.DTO:CreateCustomerInputModel", x =>
            {
                x.Durable = false;
                x.AutoDelete = true;
                x.ExchangeType = "fanout";
                x.RoutingKey = "customer-created-masstransit";
            });
        });

        //config.ConfigureEndpoints(context);
    });

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
