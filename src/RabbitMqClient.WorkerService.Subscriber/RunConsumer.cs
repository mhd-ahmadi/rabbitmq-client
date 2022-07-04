using Newtonsoft.Json.Linq;
using RabbitMqClient.Core.Messaging;
using RabbitMqClient.Core.Models;
using RabbitMqClient.WorkerService.Subscriber.Messages;
using System.Text;

namespace RabbitMqClient.WorkerService.Subscriber
{
    public class RunConsumer : BackgroundService
    {
        private readonly ILogger<RunConsumer> _logger;
        private readonly IServiceProvider _serviceProvider;

        public RunConsumer(ILogger<RunConsumer> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested)
                return Task.CompletedTask;

            _logger.LogInformation("Consumer is running ready to serve the message...");

            using IServiceScope scope = _serviceProvider.CreateScope();
            var rabbitProvider = scope.ServiceProvider.GetRequiredService<IRabbitMqProvider>();

            var channelResult = rabbitProvider.CreateChannel("ex:order", "direct", "order.created", "order.created");
            rabbitProvider.SubscribeMessage<OrderCreatedMessage>(channelResult, (message) =>
            {
                Console.WriteLine("\n---------- New message recieved! ----------");

                Console.WriteLine($"{nameof(OrderCreatedMessage.OrderId)}: {message.Body.OrderId}");
                Console.WriteLine($"{nameof(OrderCreatedMessage.OrderTotal)}: {message.Body.OrderTotal}");
                Console.WriteLine($"{nameof(OrderCreatedMessage.OrderDate)}: {message.Body.OrderDate}");
                Console.WriteLine($"_CorrelationId: {message.CorrelationId}");

                Console.WriteLine("*********************************************");
            });

            return Task.CompletedTask;
        }
    }
}