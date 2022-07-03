using RabbitMqClient.Core;
using RabbitMqClient.Core.Models;
using RabbitMqClient.WebApi.Publisher;
using RabbitMqClient.WebApi.Publisher.Messages;
using RabbitMqClient.WebApi.Publisher.Models;
using RabbitMqClient.WebApi.Publisher.Publishers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddClientCore();
builder.Services.AddAppServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapPost("/api/order", async (OrderCreateModel model, IOrderCreatePublisher publisher) =>
{
    var data = new MessageBase<OrderCreatedMessage>
    {
        CorrelationId = Guid.NewGuid().ToString(),
        Body = new OrderCreatedMessage
        {
            OrderId = model.OrderId,
            OrderTotal = model.OrderTotal,
            OrderDate = DateTimeOffset.Now,
        }
    };
    await publisher.PublishAsync(data);
    return data;
})
.WithName("CreateOrder");

app.Run();