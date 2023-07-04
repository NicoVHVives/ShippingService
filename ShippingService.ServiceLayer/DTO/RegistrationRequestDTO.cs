﻿using System.ComponentModel.DataAnnotations;

namespace ShippingService.ServiceLayer.DTO
{
    
        public class RegistrationRequestDTO
        {
            [Required]
            public string Email { get; set; } = null!;
            [Required]
            public string Username { get; set; } = null!;
            [Required]
            public string Password { get; set; } = null!;
        }
    
}
