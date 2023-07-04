using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingService.DomainLayer.Models;

namespace ShippingService.ServiceLayer.DTO
{ 
    public class ShippingResponseDTO
    {
        
        public Guid Account { get; set; }
        public Guid Product { get; set; }

        public string Access { get; set; } = null!;
    }
}
