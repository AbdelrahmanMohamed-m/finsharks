using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DataLayer.Dtos.AccountDto
{
    public class RegisterDto
    {
        [Required]
        public string? Username { get; set; }
       
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public String? Password { get; set; }
    }
}