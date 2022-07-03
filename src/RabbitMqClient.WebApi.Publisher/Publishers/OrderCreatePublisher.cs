using RabbitMqClient.Core.Messaging;
using RabbitMqClient.Core.Models;
using RabbitMqClient.WebApi.Publisher.Messages;

namespace RabbitMqClient.WebApi.Publisher.Publishers
{
    public interface IOrderCreatePublisher
    {
        Task PublishAsync(MessageBase<OrderCreatedMessage> orderCreatedMessage);
    }

    public class OrderCreatePublisher : IOrderCreatePublisher
    {
        private readonly IRabbitMqProvider _rabbitProvider;

        public OrderCreatePublisher(IRabbitMqProvider rabbitProvider)
        {
            _rabbitProvider = rabbitProvider;
        }

        public Task PublishAsync(MessageBase<OrderCreatedMessage> orderCreatedMessage)
        {
            var channelResult = _rabbitProvider.CreateChannel("ex:order", "direct", "order.created", "order.created");
            _rabbitProvider.PublishMessage(orderCreatedMessage, channelResult);
            return Task.CompletedTask;
        }
    }
}