using DeliveryWeb.Data;
using DeliveryWeb.Utility;
using DeliveryWeb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace DeliveryWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.Manager)]
    [Area("Admin")]
    public class City : Controller
    {
        public ApplicationDbContext Db { get; set; }
        public City(ApplicationDbContext Db)
        {
            this.Db = Db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await Db.Brunshes.Where(item => item.Exist == true).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(string brunsh, string code)
        {
            if (brunsh == null)
            {
                return NotFound();

            }
            Cities_BrunshName cities_BrunshName = new Cities_BrunshName()
            {
                Cities = await Db.Cities.Where(item =>  item.Exist==true && item.Brunsh == code).ToListAsync(),
                BrunshName = brunsh,
                Code=code

            };
            if (cities_BrunshName.Cities == null)
            {
                return NotFound();
            }
            return View(cities_BrunshName);
        }

        [HttpGet]
        public async Task<IActionResult> Add(string code)
        {
            //var Claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            //if (Claim == null)
            //{
            //    return NotFound();
            //}
            //string ID = Claim.Value;
            //var user = await Db.Customers.FindAsync(ID);
            City_Province city_Province = new City_Province()
            {
                Provinces = await Db.Provinces.ToListAsync(),
                City = new Models.City(),
                Code = code
                
            };
            return View(city_Province);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(City_Province city_province)
        {
            if (ModelState.IsValid)
            {
                city_province.City.Exist = true;
                city_province.City.Brunsh = city_province.Code;
                Db.Cities.Add(city_province.City);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(city_province);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            City_Province city_Province = new City_Province()
            {
                Provinces = await Db.Provinces.ToListAsync(),
                City = await Db.Cities.FindAsync(id)

            };
            if (city_Province == null)
            {
                return NotFound();
            }
            return View(city_Province);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(City_Province city_province)
        {
            if (ModelState.IsValid)
            {
                city_province.City.Exist = true;
                Db.Cities.Update(city_province.City);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(city_province);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            City_Province city_Province = new City_Province()
            {
                Provinces = await Db.Provinces.ToListAsync(),
                City = await Db.Cities.FindAsync(id)

            };
            if (city_Province == null)
            {
                return NotFound();
            }
            return View(city_Province);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(City_Province city_province)
        {
            if (ModelState.IsValid)
            {
                city_province.City.Exist = false;
                Db.Cities.Update(city_province.City);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(city_province);
        }
    }
}
