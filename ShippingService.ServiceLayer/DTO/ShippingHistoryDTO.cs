using ShippingService.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.ServiceLayer.DTO
{
    public class ShippingHistoryDTO
    {
        public class ShippingHistory : BaseEntity
        {

            [Required]
            public Guid ClientGuid { get; set; }
            [Required]
            public Guid ItemGuid { get; set; }


        }
    }
}
