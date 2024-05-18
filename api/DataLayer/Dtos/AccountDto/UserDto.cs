using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DataLayer.Dtos.AccountDto
{
    public class UserDto
    {
        
        public String userName { get; set; } = "";

        public String email { get; set; } = "";

        public String token { get; set; } = "";
    }
}