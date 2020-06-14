using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.ViewModels;
using System.Windows;




namespace WebApplication3.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScheduleController(ApplicationDbContext context)
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
            

            var currentUser = this.User;
            
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userAddresses = _context.Addresses.ToList().Where(m => m.ApplicationUserId == currentUserId);

            var viewModel = new ScheduleViewModel()
            {
                Schedule = new Schedule(),
                UserAddresses = userAddresses
            };

            return View(viewModel);
        }

        [Authorize]
        [Route("/schedule/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var oldAddresses = _context.Addresses.ToList().Where(m => m.ApplicationUserId == currentUserId);

            var schedule = _context.Schedules.Include(c => c.UserAddress).SingleOrDefault(m => m.Id == id);

            var viewModel = new EditScheduleViewModel(schedule, oldAddresses);
            
            return View("Edit", viewModel);
        }

      



        [Authorize]
        [HttpPost]
        public IActionResult Save(Schedule schedule)
        {
            //schedule.ScheduledTime.TryParse
            var currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            
          
            var oldAddresses = _context.Addresses.ToList().Where(m => m.ApplicationUserId == currentUserId);

           
            if (!ModelState.IsValid)
            {
                var viewModel = new ScheduleViewModel(schedule, oldAddresses);
               
                return View("Index", viewModel);
                
            }
            if (schedule.Id == 0)
            {
                var scheduleUser = _context.ApplicationUsers.ToList().Single(c => c.Id == currentUserId);
                var scheduleUserName = scheduleUser.FirstName + " " + scheduleUser.LastName;

                schedule.LastModifiedUserId = currentUserId;
                schedule.State = "New";
                schedule.ApplicationUserId = currentUserId;
                schedule.RequestTime = DateTime.Now;
                schedule.Name = scheduleUserName;
                schedule.TypesOfServicesId = 1;
                schedule.TypeOfClient = _context.ApplicationUsers.ToList().Single(m => m.Id == currentUserId).ClientType;
                _context.Schedules.Add(schedule);

                _context.SaveChanges();


                return RedirectToAction("SendScheduleRegisterEmail", "EMail", new { id = schedule.Id, name = schedule.Name });

            }
            else
            {

                var scheduleInDb = _context.Schedules.Include(c => c.UserAddress).Single(c => c.Id == schedule.Id);

                scheduleInDb.UserAddressId = schedule.UserAddressId;

                scheduleInDb.ScheduledTime = schedule.ScheduledTime;

                scheduleInDb.RequestTime = DateTime.Now;

                scheduleInDb.LastModifiedUserId = currentUserId;

                scheduleInDb.Id = schedule.Id;

                scheduleInDb.State = "Edited";

                scheduleInDb.TypesOfServicesId = 1;

                _context.SaveChanges();


                return RedirectToAction("SendEditEmail", "EMail", new { id = schedule.Id, name = scheduleInDb.Name });

            }

            


        }

        [Authorize]
        [Route("/schedule/userScheduleList/")]
        public IActionResult UserScheduleList()
        {
           


            var currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userSchedules = _context.Schedules.Include(c => c.UserAddress).ToList()
                .Where(m => m.ApplicationUserId == currentUserID);


            var userAddresses = _context.Addresses.ToList().Where(c=> c.ApplicationUserId == currentUserID);

            var viewModel = new ScheduleListViewModel
            {
                UserSchedulesList = userSchedules,
                Addresses = userAddresses


            };

            return View(viewModel);
        }

       
        

      

        
        [Authorize]
        public IActionResult DeleteSchedule(int id)
        {

            var scheduleInDb = _context.Schedules.SingleOrDefault(c => c.Id == id);

            if (scheduleInDb == null)
                return BadRequest();

            _context.Schedules.Remove(scheduleInDb);
            _context.SaveChanges();


            return RedirectToAction("UserScheduleList");

        }

      
        public IActionResult TestHtml()
        {
            return View();
        }

    }
          
    
}