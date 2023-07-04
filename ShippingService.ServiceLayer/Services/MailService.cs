using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShippingService.ServiceLayer.DTO;
using System.Text;

namespace ShippingService.ServiceLayer.Services
{
    public class MailService
    {


        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public MailService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }


        public async Task<bool> SendEmail(MailMessage mail)
        {
            using(HttpClient client = _httpClientFactory.CreateClient("EmailService"))
            {

                var stringContent = new StringContent(JsonConvert.SerializeObject(mail), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_configuration.GetValue<string>("EmailService:Endpoint"), stringContent);

                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public MailMessage generateFirstShipmentMail(Guid clientID)
        {
            return new MailMessage
            {
                ClientID = clientID,
                Subject = "First free shipment has been sent out",
                Body = "bla bla bla bla bla bla"
            };

        }

        public MailMessage generateLastShipmentMail(Guid clientID)
        {
            return new MailMessage
            {
                ClientID = clientID,
                Subject = "Last free shipment has been sent out",
                Body = "bla bla bla bla bla bla"
            };
        }
    }

}




