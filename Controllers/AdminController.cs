using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Services;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        

        [Authorize]
        public IActionResult Index()
        {
            var scheduleList = _context.Schedules.Include(c => c.TypesOfServices).ToList();
            

            var addressList = _context.Addresses.ToList();

            var viewModel = new ScheduleListViewModel
            {
                UserSchedulesList = scheduleList,
                Addresses = addressList,
              
            };

            return View(viewModel);
        }

        public IActionResult AcceptSchedule(int id, string state)
        {
            var scheduleInDb = _context.Schedules.Single(c => c.Id == id);

            scheduleInDb.State = state;

            

            _context.SaveChanges();

            return RedirectToAction("SendStateChangeEmail", "EMail", new { Id = id, State = state});
        }

        

        public IActionResult DeleteSchedule(int id)
        {

            var scheduleInDb = _context.Schedules.SingleOrDefault(c => c.Id == id);

            if (scheduleInDb == null)
                return BadRequest();

            _context.Schedules.Remove(scheduleInDb);
            _context.SaveChanges();


            return RedirectToAction("Index");

        }

        //modified route
        [Route("/admin/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var currentUser = this.User;

            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var schedule = _context.Schedules.Include(c => c.UserAddress).SingleOrDefault(m => m.Id == id);

            var oldAddresses = _context.Addresses.ToList().Where(m => m.ApplicationUserId == schedule.ApplicationUserId);

            var viewModel = new EditScheduleViewModel(schedule, oldAddresses);

            return View("Edit", viewModel);
        }

        [HttpPost]
        public IActionResult Save(Schedule schedule)
        {
            var currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (schedule.ApplicationUserId == null)
            {
                schedule.ApplicationUserId = currentUserId;
            }
           
            var userAddresses = _context.Addresses.ToList().Where(m => m.ApplicationUserId == schedule.ApplicationUserId);


            if (!ModelState.IsValid)
            {
                var viewModel = new EditScheduleViewModel(schedule, userAddresses);

                return View("Edit", viewModel);
            }
            if (schedule.Id == 0)
            {

                schedule.State = "New";
                schedule.LastModifiedUserId = currentUserId;
                schedule.RequestTime = DateTime.Now;

                _context.Schedules.Add(schedule);
            }
            else
            {

                var scheduleInDb = _context.Schedules.Include(c => c.UserAddress).Single(c => c.Id == schedule.Id);

                scheduleInDb.LastModifiedUserId = schedule.LastModifiedUserId;

                scheduleInDb.ScheduledTime = schedule.ScheduledTime;    

                scheduleInDb.RequestTime = DateTime.Now;    

                //scheduleInDb.ApplicationUserId = currentUserId;

                scheduleInDb.Id = schedule.Id;

                scheduleInDb.State = "Edited by Admin";
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}