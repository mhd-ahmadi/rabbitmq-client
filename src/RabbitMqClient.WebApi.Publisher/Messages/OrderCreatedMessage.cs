namespace RabbitMqClient.WebApi.Publisher.Messages
{
    public class OrderCreatedMessage
    {
        public int OrderId { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTimeOffset OrderDate { get; set; }
    }
}
