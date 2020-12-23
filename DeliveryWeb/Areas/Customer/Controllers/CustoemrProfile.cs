using DeliveryWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeliveryWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CustoemrProfile : Controller
    {
        public ApplicationDbContext Db { get; }
        public CustoemrProfile(ApplicationDbContext Db)
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
            var user = await Db.Customers.FindAsync(ID);
            return View(user);
        }

    }
}
