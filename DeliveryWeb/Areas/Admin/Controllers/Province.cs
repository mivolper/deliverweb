using DeliveryWeb.Data;
using DeliveryWeb.Utility;
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
    public class Province : Controller
    {
        public ApplicationDbContext Db { get; set; }
        public Province(ApplicationDbContext Db)
        {
            this.Db = Db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await Db.Provinces.Where(item => item.Exist == true).ToListAsync());
        }

        [HttpGet]
        public IActionResult Add()
        {
            //var Claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            //if (Claim == null)
            //{
            //    return NotFound();
            //}
            //string ID = Claim.Value;
            //var user = await Db.Customers.FindAsync(ID);
            Models.Province province = new Models.Province();
            return View(province);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Models.Province province)
        {
            if (ModelState.IsValid)
            {
                province.Exist = true;
                Db.Provinces.Add(province);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(province);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await Db.Provinces.FindAsync(id);
            if (province == null)
            {
                return NotFound();
            }
            return View(province);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Models.Province province)
        {
            if (ModelState.IsValid)
            {
                province.Exist = true;
                Db.Provinces.Update(province);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(province);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await Db.Provinces.FindAsync(id);

            if (province == null)
            {
                return NotFound();
            }
            return View(province);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Models.Province province)
        {
            if (ModelState.IsValid)
            {
                province.Exist = false;
                Db.Provinces.Update(province);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(province);
        }
    }
}
