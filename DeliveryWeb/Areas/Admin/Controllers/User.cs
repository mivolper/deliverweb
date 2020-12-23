using DeliveryWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DeliveryWeb.Utility;
using Microsoft.AspNetCore.Authorization;

namespace DeliveryWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.Manager)]
    [Area("Admin")]
    public class User : Controller
    {
        private  ApplicationDbContext Db { get; set;}
        public User(ApplicationDbContext Db)
        {
            this.Db = Db;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            if (Claim == null)
            {
                return NotFound();
            }
            string ID = Claim.Value;
            return View(await Db.Customers.Where(item => item.Id != ID && item.Role != SD.User).ToListAsync());
        }

        public async Task<IActionResult> Lock(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var user = await Db.Customers.FindAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            if(user.LockoutEnd == null || user.LockoutEnd < DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            else
            {
                user.LockoutEnd = null;
            }
            await Db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
