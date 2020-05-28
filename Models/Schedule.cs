using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please choose a valid format, or a time frame in the future.")]
        [ScheduledTimeIsFutureValidation]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        
        public DateTime ScheduledTime { get; set; }

        //User that created the schedule.
        public string ApplicationUserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime? RequestTime { get; set; }

        public UserAddress UserAddress { get; set; }

        [Required(ErrorMessage = "Please select a valid address.")]
        public byte UserAddressId { get; set; }

        public string State { get; set; }

        public string Name { get; set; }

        public string Observations { get; set; }

        //User that last modified the schedule, be it admin or user.
        public string LastModifiedUserId { get; set; }

        public TypesOfServices TypesOfServices { get; set; }
        public string TypeOfClient { get; set; }
        public int? TypesOfServicesId { get; set; }

    }
}
