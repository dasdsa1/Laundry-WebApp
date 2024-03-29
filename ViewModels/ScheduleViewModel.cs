﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3
{
    public class ScheduleViewModel
    {
        /* public int Id { get; set; }

         public DateTime ScheduledTime { get; set; }

        public string ApplicationUserId { get; set; }

         public DateTime? RequestTime { get; set; }

         public UserAddress RequestAddress { get; set; }

         public byte RequestAddressId { get; set; }*/

        public IEnumerable<UserAddress> UserAddresses { get; set; }

        public Schedule Schedule { get; set; }

        public ScheduleViewModel()
        {

        }

        public ScheduleViewModel(Schedule schedule, IEnumerable<UserAddress> addresses)
        {
            Schedule = new Schedule
            {
                Id = schedule.Id,
                ScheduledTime = schedule.ScheduledTime,
                ApplicationUserId = schedule.ApplicationUserId,
                RequestTime = schedule.RequestTime,
                UserAddressId = schedule.UserAddressId

            };
            
            UserAddresses = addresses;


        }


        public string Title
        {
            get
            {
                return Schedule.Id == 0 ? "New Schedule" : "Edit Schedule";
            }
        }
    }
}
