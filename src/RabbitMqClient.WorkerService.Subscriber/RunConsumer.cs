using Newtonsoft.Json.Linq;
using RabbitMqClient.Core.Messaging;
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
            var consumer = rabbitProvider.GetBasicConsumer(channelResult, autoAck: true);

            consumer.Received += (ch, ea) =>
            {
                Console.WriteLine($"\n----- New Order Created -----");
                string body = Encoding.UTF8.GetString(ea.Body.ToArray());
                JObject parsed = JObject.Parse(body);
                foreach (var pair in parsed)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
                Console.WriteLine($"_CorrelationId: {ea.BasicProperties.CorrelationId}");
                Console.WriteLine("******************************");

                // if you want to acknowledge message manually
                //channelResult.Channel!.BasicAck(ea.DeliveryTag, true);
            };

            return Task.CompletedTask;
        }
    }
}