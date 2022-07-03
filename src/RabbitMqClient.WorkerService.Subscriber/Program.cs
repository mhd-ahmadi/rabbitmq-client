using RabbitMqClient.Core;
using RabbitMqClient.Core.Models;
using RabbitMqClient.WorkerService.Subscriber;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddClientCore();
        services.AddHostedService<RunConsumer>();
    })
    .Build();

await host.RunAsync();
