namespace RabbitMqClient.WebApi.Publisher.Models
{
    public class OrderCreateModel
    {
        public int OrderId { get; set; }
        public decimal OrderTotal { get; set; }
    }
}