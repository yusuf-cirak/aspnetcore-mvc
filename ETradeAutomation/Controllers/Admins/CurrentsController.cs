using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreMvcOnlineTicariOtomasyon.DataAccess.EntityFramework;
using CoreMvcOnlineTicariOtomasyon.Models;

namespace CoreMvcOnlineTicariOtomasyon.Controllers.Admins
{
    [Route("[controller]/[action]")]
    //[Authorize]
    [ControllerName("currents")]
    public class CurrentsController : Controller
    {
        private readonly ETradeAutomationContext _context;

        public CurrentsController(ETradeAutomationContext context)
        {
            _context = context;
        }

        // GET: Currents
        [HttpGet, ActionName("index")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.Currents.Where(x=>x.Status==true).ToListAsync());
        }

        // GET: Currents/Details/5
        [HttpGet, ActionName("details")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var current = await _context.Currents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (current == null)
            {
                return NotFound();
            }

            return View(current);
        }

        // GET: Currents/Create
        [HttpGet, ActionName("create")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Currents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("create")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,City,Email,Status")] Current current)
        {
            if (ModelState.IsValid)
            {
                _context.Add(current);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(current);
        }

        // GET: Currents/Edit/5
        [HttpGet, ActionName("edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var current = await _context.Currents.FindAsync(id);
            if (current == null)
            {
                return NotFound();
            }
            return View(current);
        }

        // POST: Currents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("edit")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,City,Email,Status")] Current current)
        {
            if (id != current.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(current);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrentExists(current.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(current);
        }

        // GET: Currents/Delete/5
        [HttpGet, ActionName("delete")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var current = await _context.Currents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (current == null)
            {
                return NotFound();
            }

            return View(current);
        }

        // POST: Currents/Delete/5
        [HttpPost, ActionName("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var current = await _context.Currents.FindAsync(id);
            _context.Currents.Remove(current);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet, ActionName("sales")]

        public IActionResult Sales(int id)
        {
            var values = _context.SaleMovements.Where(x => x.CurrentId == id).Include(y=>y.Product).Include(b=>b.Employee).ToList();
            var current = _context.Currents.Where(x => x.Id == id).Select(y => y.FirstName + " " + y.LastName).FirstOrDefault();
            ViewBag.current = current;
            return View(values);
        }
        

        private bool CurrentExists(int id)
        {
            return _context.Currents.Any(e => e.Id == id);
        }


    }
}
