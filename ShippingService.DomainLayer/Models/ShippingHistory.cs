using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.DomainLayer.Models
{
    public class ShippingHistory :BaseEntity
    {


        [Required]
        public string WebshopID { get; set; }
        [Required]
        public Guid ClientGuid { get; set; }
        [Required]
        public Guid ItemGuid { get; set; }

        [Required]
        public Guid TransactionGuid { get; set; }
    }
}
