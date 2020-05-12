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

        public int Id { get; set; }

        public DateTime ScheduledTime { get; set; }


        public string ApplicationUserId { get; set; }

        public DateTime RequestTime { get; set; }

        public UserAddress UserAddress { get; set; }

        public byte UserAddressId { get; set; }

        public IEnumerable<UserAddress> UserAddresses { get; set; }


        public EditScheduleViewModel(Schedule schedule, IEnumerable<UserAddress> addresses)
        {

            Id = schedule.Id;
            ScheduledTime = schedule.ScheduledTime;
            ApplicationUserId = schedule.ApplicationUserId;
            RequestTime = schedule.RequestTime;
            UserAddressId = schedule.UserAddressId;
            UserAddresses = addresses;


        }

        
    }
}
