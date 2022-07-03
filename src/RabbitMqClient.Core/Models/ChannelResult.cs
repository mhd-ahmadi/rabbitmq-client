using RabbitMQ.Client;

namespace RabbitMqClient.Core.Models
{
    public class ChannelResult
    {
        public ChannelResult(IModel channel)
        {
            Channel = channel;
        }

        public IModel? Channel { get; set; }
        public string ExchangeName { get; set; } = "";
        public string QueneName { get; set; } = "";
        public string RoutingKey { get; set; } = "";
    }
}
