using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Dtos
{
    public class ApplicationUserDto
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Select a client type, Personal or Company.")]
        public string ClientType { get; set; }
    }
}
