using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DeliveryWeb.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DelvieryWebSite.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class CustomerOrder : Controller
    {
        public ApplicationDbContext Db { get; }

        public CustomerOrder(ApplicationDbContext Db)
        {
            this.Db = Db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            //if (Claim == null)
            //{
            //    return ;
            //}
            string ID = Claim.Value;
            var user = await Db.Customers.FindAsync(ID);
            return View(await Db.CustomerOrders.Include(m => m.City).Where(item => item.CustomerCode == user.Code).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> AddOrder()
        {
            var Claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            if (Claim == null)
            {
                return NotFound();
            }
            string ID = Claim.Value;
            var user = await Db.Customers.FindAsync(ID);
            City_CustomerOrder_VM city_CustomerOrder_VM = new City_CustomerOrder_VM()
            {
                Cities = await Db.Cities.ToListAsync(),
                CustomerOrder = new DeliveryWeb.Models.CustomerOrder(),
            };
            city_CustomerOrder_VM.CustomerOrder.CustomerCode = user.Code;
            return View(city_CustomerOrder_VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrder(City_CustomerOrder_VM city_CustomerOrder_VM)
        {
            if (ModelState.IsValid)
            {
                Db.CustomerOrders.Add(city_CustomerOrder_VM.CustomerOrder);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(city_CustomerOrder_VM.CustomerOrder);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            City_CustomerOrder_VM city_CustomerOrder_VM = new City_CustomerOrder_VM()
            {
                Cities = await Db.Cities.ToListAsync(),
                CustomerOrder = await Db.CustomerOrders.FindAsync(id)
            };
            
            if (city_CustomerOrder_VM.CustomerOrder == null)
            {
                return NotFound();
            }
            return View(city_CustomerOrder_VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(City_CustomerOrder_VM city_CustomerOrder_VM)
        {
            if (ModelState.IsValid)
            {
                Db.CustomerOrders.Update(city_CustomerOrder_VM.CustomerOrder);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(city_CustomerOrder_VM.CustomerOrder);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customerorder = await Db.CustomerOrders.Include(m => m.City).SingleOrDefaultAsync(item => item.ID_SubOrder == id);

            if (customerorder == null)
            {
                return NotFound();
            }
            return View(customerorder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeliveryWeb.Models.CustomerOrder customerOrder)
        {
            if (ModelState.IsValid)
            {
                Db.CustomerOrders.Remove(customerOrder);
                await Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerOrder);
        }
    }
}
