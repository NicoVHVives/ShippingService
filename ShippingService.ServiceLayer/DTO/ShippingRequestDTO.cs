using System.ComponentModel.DataAnnotations;

namespace ShippingService.ServiceLayer.DTO
{
    public class ShippingRequestDTO
    {
        [Required]
        public Guid TransactionGuid { get; set; }
        [Required]
        public Guid ClientGuid { get; set; }
        [Required]
        public Guid ItemGuid { get; set; }



    }
}
