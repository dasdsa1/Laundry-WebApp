using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class ScheduledTimeIsFutureValidation : RangeAttribute
    {
        public ScheduledTimeIsFutureValidation() :
            base(typeof(DateTime), DateTime.Now.AddHours(2).ToShortDateString(), DateTime.Now.AddDays(180).ToShortDateString()) {}
    }
}



