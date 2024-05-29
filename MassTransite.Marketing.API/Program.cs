using MassTransit;
using MassTransite.Marketing.API.Services;
using MassTransite.Marketing.API.Subscribers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<INotification, Notification>();

builder.Services.AddMassTransit(c =>
{
    c.AddConsumer<CustpmerCreatedSubscriber>();

    c.UsingRabbitMq((context, config) =>
    {
        config.ConfigureEndpoints(context);
    });
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

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
