using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqClient.Core.Models;

namespace RabbitMqClient.Core.Messaging
{
    public interface IRabbitMqProvider
    {
        ChannelResult CreateChannel(string exchangeName, string exchangeType, string queueName, string routingKey = "");
        void PublishMessage<T>(MessageBase<T> message, ChannelResult channelInfo) where T : class;
        EventingBasicConsumer GetBasicConsumer(ChannelResult channelInfo, bool autoAck = true);
    }
}