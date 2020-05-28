using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.ViewModels
{
    public class EditScheduleViewModel
    {

        public Schedule Schedule { get; set; }
       

        public IEnumerable<UserAddress> UserAddresses { get; set; }

       


        public EditScheduleViewModel(Schedule schedule, IEnumerable<UserAddress> addresses)
        {
            Schedule = new Schedule
            {

                Id = schedule.Id,
                ScheduledTime = schedule.ScheduledTime,
                ApplicationUserId = schedule.ApplicationUserId,
                RequestTime = schedule.RequestTime,
                UserAddressId = schedule.UserAddressId,
                Observations = schedule.Observations
            };

            UserAddresses = addresses;

        }

        
    }
}
