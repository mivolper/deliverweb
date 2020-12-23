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
    [Authorize(Roles =SD.Manager)]
    [Area("Admin")]
    public class Brunsh : Controller
    {
        public ApplicationDbContext Db { get; set; }
        public Brunsh(ApplicationDbContext Db)
        {
            this.Db = Db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await Db.Brunshes.Where(item => item.Exist == true).ToListAsync());
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
            Models.Brunsh brunsh = new Models.Brunsh();
            return View(brunsh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Models.Brunsh brunsh)
        {
            if (ModelState.IsValid)
            {
                brunsh.Exist = true;
                Db.Brunshes.Add(brunsh);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brunsh);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brunsh = await Db.Brunshes.FindAsync(id);
            if (brunsh == null)
            {
                return NotFound();
            }
            return View(brunsh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Models.Brunsh brunsh)
        {
            if (ModelState.IsValid)
            {
                brunsh.Exist = true;
                Db.Brunshes.Update(brunsh);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brunsh);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brunsh = await Db.Brunshes.FindAsync(id);

            if (brunsh == null)
            {
                return NotFound();
            }
            return View(brunsh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Models.Brunsh brunsh)
        {
            if (ModelState.IsValid)
            {
                brunsh.Exist = false;
                Db.Brunshes.Update(brunsh);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brunsh);
        }

    }
}
