using DeliveryWeb.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryWeb.Models;
using DeliveryWeb.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DeliveryWeb.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class Movment : Controller
    {
        public ApplicationDbContext Db { get; }

        public Movment(ApplicationDbContext Db)
        {
            this.Db = Db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            string ID = Claim.Value;
            var user =await Db.Customers.FindAsync(ID);
            return View(await Db.Orders.Where(item => item.Customer == user.Code && item.Exist == true).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(string barcode)
        {
            if(barcode == null)
            {
                return NotFound();

            }
            var order = await Db.Orders.SingleOrDefaultAsync(item => item.Barcode == barcode);
            if(order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}
