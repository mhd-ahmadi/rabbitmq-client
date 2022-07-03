namespace RabbitMqClient.Core.Models
{
    public class MessageBase<T>
    {
        public string CorrelationId { get; set; } = "";
        public T Body { get; set; }
    }
}