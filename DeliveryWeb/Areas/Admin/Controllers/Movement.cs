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
    public class Movement : Controller
    {
        public ApplicationDbContext Db { get; set; }
        public Movement(ApplicationDbContext Db)
        {
            this.Db = Db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await Db.Movements.Where(item => item.Exist == true).ToListAsync());
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
            Models.Movement movement = new Models.Movement();
            return View(movement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Models.Movement movement)
        {
            if (ModelState.IsValid)
            {
                movement.Exist = true;
                Db.Movements.Add(movement);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movement);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movement = await Db.Movements.FindAsync(id);
            if (movement == null)
            {
                return NotFound();
            }
            return View(movement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Models.Movement movement)
        {
            if (ModelState.IsValid)
            {
                movement.Exist = true;
                Db.Movements.Update(movement);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movement);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movement = await Db.Movements.FindAsync(id);

            if (movement == null)
            {
                return NotFound();
            }
            return View(movement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Models.Movement movement)
        {
            if (ModelState.IsValid)
            {
                movement.Exist = false;
                Db.Movements.Update(movement);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movement);
        }
    }
}
