using RabbitMQ.Client;

namespace RabbitMqClient.Core.Messaging
{
    public interface IRabbitMqConnection
    {
        /// <summary>
        /// Get The connection of RabbitMQ server
        /// </summary>
        /// <param name="connectionStringName">The name of connection string in appsettings.json file</param>
        /// <returns>IConnection object</returns>
        IConnection GetConnection(string connectionStringName = "RabbitMq");
    }
}