using RabbitMQ.Client;

namespace RabbitMqClient.Core.Messaging
{
    public interface IRabbitMqConnection
    {
        IConnection GetConnection(string connectionStringName = "RabbitMq");
    }
}
