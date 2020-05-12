using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;

namespace WebApplication3.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIScheduleController : ControllerBase
    {

        private ApplicationDbContext _context;

        public APIScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult DeleteSchedule(int id)
        {

            var scheduleInDb = _context.Schedules.SingleOrDefault(c => c.Id == id);

            if (scheduleInDb == null)
                return BadRequest();

            _context.Schedules.Remove(scheduleInDb);
            _context.SaveChanges();

            
            return Ok();

        }
    }
}