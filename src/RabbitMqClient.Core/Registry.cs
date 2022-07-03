using Microsoft.Extensions.DependencyInjection;
using RabbitMqClient.Core.Messaging;

namespace RabbitMqClient.Core
{
    public static class Registry
    {
        public static IServiceCollection AddClientCore(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();

            services.AddScoped<IRabbitMqProvider, RabbitMqProvider>();

            return services;
        }
    }
}
