using DeliveryWeb.Data;
using DeliveryWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class DelivryPrice : Controller
    {
        public ApplicationDbContext Db { get; }

        public DelivryPrice(ApplicationDbContext Db)
        {
            this.Db = Db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await Db.Brunshes.Where(item => item.Exist == true).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(string brunsh,string code)
        {
            if (brunsh == null)
            {
                return NotFound();

            }
            Cities_BrunshName cities_BrunshName = new Cities_BrunshName()
            {
                Cities = await Db.Cities.Where(item => item.Exist == true && item.Brunsh == code).ToListAsync(),
                BrunshName=brunsh

            };
            if (cities_BrunshName.Cities == null)
            {
                return NotFound();
            }
            return View(cities_BrunshName);
        }

    }
}
