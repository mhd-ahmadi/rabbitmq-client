using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqClient.Core.Models;

namespace RabbitMqClient.Core.Messaging
{
    public interface IRabbitMqProvider
    {
        /// <summary>
        /// Create channel with specific information
        /// </summary>
        /// <param name="exchangeName">The name of exchange</param>
        /// <param name="exchangeType">Type of exchange [direct, fanout, topic, header]</param>
        /// <param name="queueName">The name of queue</param>
        /// <param name="routingKey">The routing of queue</param>
        /// <returns></returns>
        ChannelResult CreateChannel(string exchangeName, string exchangeType, string queueName, string routingKey = "");

        /// <summary>
        /// Publish message
        /// </summary>
        /// <typeparam name="T">The generic type of message</typeparam>
        /// <param name="message">The body of message</param>
        /// <param name="channelInfo">The information of channel create on CreateChannel method</param>
        void PublishMessage<T>(MessageBase<T> message, ChannelResult channelInfo) where T : class;

        /// <summary>
        /// Subscribe message from queue
        /// </summary>
        /// <typeparam name="T">The typeof message</typeparam>
        /// <param name="channelInfo">The information of channel create on CreateChannel method</param>
        /// <param name="getMessageFunc">A function for calling after getting response</param>
        /// <param name="autoAck">Set status of acknowledege the message</param>
        void SubscribeMessage<T>(ChannelResult channelInfo, Action<MessageBase<T>> getMessageFunc, bool autoAck = true);
    }
}