using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.ViewModels
{
    public class ScheduleListViewModel
    {
        public IEnumerable<Schedule> UserSchedulesList { get; set; }

        public IEnumerable<UserAddress> Addresses { get; set; }

        

    }

    
}
