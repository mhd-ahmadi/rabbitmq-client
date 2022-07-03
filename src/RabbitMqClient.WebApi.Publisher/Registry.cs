using RabbitMqClient.WebApi.Publisher.Publishers;

namespace RabbitMqClient.WebApi.Publisher
{
    public static class Registry
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddTransient<IOrderCreatePublisher, OrderCreatePublisher>();
            return services;
        }
    }
}
