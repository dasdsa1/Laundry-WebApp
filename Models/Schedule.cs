using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public DateTime ScheduledTime { get; set; }

        
        public string ApplicationUserId { get; set; }

        public DateTime RequestTime { get; set; }

        public UserAddress UserAddress { get; set; }

        public byte UserAddressId { get; set; }

        public string State { get; set; }

        
        
    }
}
