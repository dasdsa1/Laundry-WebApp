using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Dtos
{
    public class UserAddressDto
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public string ApplicationUserId { get; set; }

    }
}
