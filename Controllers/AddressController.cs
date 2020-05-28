using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    public class AddressController : Controller
    {
        private ApplicationDbContext _context;

        public AddressController(ApplicationDbContext context)
        {
            _context = context;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public IActionResult Index()
        {
            return View("AddressList");
        }

        [Authorize]
        public IActionResult NewAddress()
        {
            var newAddress = new NewAddressViewModel();
            return View(newAddress);
        }

        [Authorize]
        public IActionResult SaveAddress(UserAddress userAddress)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewAddressViewModel
                {
                    UserAddress = userAddress
                };

                return View("NewAddress", viewModel);
            }

            if (userAddress.Id == 0)
            {
                var currentUser = this.User;
                var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                userAddress.ApplicationUserId = currentUserId;

                _context.Addresses.Add(userAddress);
            }
            else
            {
                var addressInDb = _context.Addresses.Single(m => m.Id == userAddress.Id);
                addressInDb.Address = userAddress.Address;

            }
            _context.SaveChanges();

            return RedirectToAction("UserScheduleList", "Schedule");
        }

        [Authorize]
        public IActionResult EditAddress(int id)
        {
            var currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userAddress = _context.Addresses.Single(w => w.Id == id);

            var viewModel = new NewAddressViewModel
            {
                UserAddress = userAddress
            };

            return View("NewAddress", viewModel);
        }


        [Authorize]
        public IActionResult DeleteAddress(byte id)
        {
            var addressInDb = _context.Addresses.Single(c => c.Id == id);

            if (addressInDb == null)
                return BadRequest();

            if (_context.Schedules.ToList().Any(m => m.UserAddressId == id))
            {
                //Make a view or check the correct flow.
                return Content("There's a schedule using this address, please edit the schedule before removing it.");
            }

            _context.Addresses.Remove(addressInDb);
            _context.SaveChanges();



            return RedirectToAction("AddressList");
        }


        [Authorize]
        public IActionResult AddressList()
        {
            var currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userAddresses = _context.Addresses.ToList().Where(c => c.ApplicationUserId == currentUserId);

            var addressList = new AddressListViewModel
            {
                Addresses = userAddresses
            };

            return View(addressList);
        }
    }
}