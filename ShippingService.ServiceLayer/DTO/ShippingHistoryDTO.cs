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
        

            
            public Guid ClientGuid { get; set; }
          
            public Guid ItemGuid { get; set; }

            public DateTime CreatedDate { get; set; }
        
    }
}
