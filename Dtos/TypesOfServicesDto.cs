using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Dtos
{
    public class TypesOfServicesDto
    {
        public int Id { get; set; }

        public string ServiceType { get; set; }

        //Make a tick box for the bellow.
        //Edredon
        public bool Duvet { get; set; }

        public bool Blanket { get; set; }

        public bool Carpet { get; set; }
    }
}
