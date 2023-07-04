namespace ShippingService.ServiceLayer.DTO
{
    public class MailMessage
    {

        public Guid ClientID { get; set; }

           public string Subject { get; set; } = null!;

        public string Body { get; set; } = null!;
    }
}
