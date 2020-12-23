using DeliveryWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeliveryWeb.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class Profile : Controller
    {
        public ApplicationDbContext Db { get; }
        private readonly UserManager<IdentityUser> _userManager;

        public Profile(ApplicationDbContext Db, UserManager<IdentityUser> userManager)
        {
            this.Db = Db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var Claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);

            string ID = Claim.Value;
            var user = await Db.Customers.FindAsync(ID);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Models.Customer customer)
        {
            if (ModelState.IsValid)
            {
                var Claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);

                string ID = Claim.Value;
                var user = await Db.Customers.FindAsync(ID);
                user.Name = customer.Name;
                user.Phone1 = customer.Phone1;
                user.Phone2 = customer.Phone2;
                user.Address = customer.Address;

                Db.Customers.Update(user);
                await Db.SaveChangesAsync();
                //await _userManager.UpdateAsync(customer);
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            return View(customer);
        }

    }
}
