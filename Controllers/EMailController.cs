using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class EMailController : Controller
    {

        private IEmailSender _emailSender;

        public EMailController(IEmailSender emailSender)

        {

            _emailSender = emailSender;

        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult SendEmail()

        {

            var toAddress = HttpContext.Request.Form["toAddress"].FirstOrDefault();

            var subject = HttpContext.Request.Form["subject"].FirstOrDefault();

            var body = HttpContext.Request.Form["body"].FirstOrDefault();



            _emailSender.Send(toAddress, subject, body);



            return RedirectToAction("Index", "Home");

        }

        public IActionResult SendEditEmail(int id, string name)
        {
            //Edited schedule.
           
                _emailSender.Send("p4ulo.st@gmail.com", $"Schedule {id} was edited for the user {name}", $"Dear Jose, \n \n    A schedule was edited with the Id: {id}, for the user {name}.");

            

            return RedirectToAction("UserScheduleList", "Schedule");

        }
        public IActionResult SendScheduleRegisterEmail(int id, string name)
        {
            
            //created schedule.
            
                _emailSender.Send("p4ulo.st@gmail.com", $"A New Schedule was created with the Id: {id} for the user {name}", $"Dear Jose, \n \n    The schedule {id} was created for the user {name}.");
            

            return RedirectToAction("UserScheduleList", "Schedule");

        }

        public IActionResult SendStateChangeEmail(int id, string state)
        {
            _emailSender.Send($"p4ulo.st@gmail.com", "Your request status has changed", $"Dear Blah, your the status of the request {id} has changed to {state}");

            return RedirectToAction("Index", "Admin");
        }

    }
}