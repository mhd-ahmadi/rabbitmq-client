using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace RabbitMqClient.Core.Messaging
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly IConfiguration _configuration;

        public RabbitMqConnection(IConfiguration configuration)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(IConfiguration));
        }

        public IConnection GetConnection(string connectionStringName = "RabbitMq")
        {
            var connectionString = _configuration.GetConnectionString(connectionStringName);
            if (string.IsNullOrEmpty(connectionString))
                throw new NullReferenceException("RabbitMq connection string is null or empty!");

            var factory = new ConnectionFactory { Uri = new Uri(connectionString) };
            return factory.CreateConnection();
        }
    }
}
