using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqClient.Core.Models;
using System.Text;

namespace RabbitMqClient.Core.Messaging
{
    public class RabbitMqProvider : IRabbitMqProvider
    {
        private readonly IRabbitMqConnection _rabbitMqConnection;
        private readonly IConnection _connection;

        public RabbitMqProvider(IRabbitMqConnection rabbitMqConnection)
        {
            _rabbitMqConnection = rabbitMqConnection ?? throw new ArgumentNullException(nameof(IRabbitMqConnection));
            _connection = _rabbitMqConnection.GetConnection();
        }

        public ChannelResult CreateChannel(string exchangeName, string exchangeType, string queueName, string routingKey = "")
        {
            var channel = _connection.CreateModel();

            channel.ExchangeDeclare(
                exchange: exchangeName,
                type: exchangeType,
                durable: true,
                autoDelete: false);

            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false);

            channel.QueueBind(
                queue: queueName,
                exchange: exchangeName,
                routingKey: routingKey);

            return new ChannelResult(channel) { ExchangeName = exchangeName, QueneName = queueName, RoutingKey = routingKey };
        }

        public void PublishMessage<T>(MessageBase<T> message, ChannelResult channelInfo) where T : class
        {
            var channel = channelInfo.Channel;
            if (channel == null)
                throw new NullReferenceException(nameof(channel));

            var props = channel.CreateBasicProperties();
            props.CorrelationId = message.CorrelationId;
            props.Headers = new Dictionary<string, object>()
            {
                { "CorrelationId", message.CorrelationId }
            };

            var json = JsonConvert.SerializeObject(message.Body);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(
                exchange: channelInfo.ExchangeName,
                routingKey: channelInfo.RoutingKey,
                basicProperties: props,
                body: body
                );
        }

        public void SubscribeMessage<T>(ChannelResult channelInfo, Action<MessageBase<T>> getMessageFunc, bool autoAck = true)
        {
            var channel = channelInfo.Channel;
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: channelInfo.QueneName, autoAck: autoAck, consumer);
            consumer.Received += (sender, args) =>
            {
                var bodyArray = args.Body.ToArray();
                string bodyString = Encoding.UTF8.GetString(bodyArray);
                var messageBody = JsonConvert.DeserializeObject<T>(bodyString);
                var result = new MessageBase<T>()
                {
                    Body = messageBody!,
                    CorrelationId = args.BasicProperties.CorrelationId,
                };
                if (!autoAck)
                    channel!.BasicAck(args.DeliveryTag, false);
                getMessageFunc(result);
            };
        }
    }
}