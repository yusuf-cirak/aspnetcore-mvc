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
    [ControllerName("salemovements")]
    public class SaleMovementsController : Controller
    {
        private readonly ETradeAutomationContext _context;

        public SaleMovementsController(ETradeAutomationContext context)
        {
            _context = context;
        }

        // GET: SaleMovements
        [HttpGet, ActionName("index")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.SaleMovements.Include(y=>y.Current).Include(x=>x.Employee).Include(z=>z.Product).ToListAsync());
        }

        // GET: SaleMovements/Details/5
        [HttpGet, ActionName("details")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleMovement = await _context.SaleMovements.Include(x=>x.Product).Include(y=>y.Current)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleMovement == null)
            {
                return NotFound();
            }

            return View(saleMovement);
        }

        // GET: SaleMovements/Create
        [HttpGet, ActionName("create")]

        public IActionResult Create()
        {
            List<SelectListItem> product = (from x in _context.Products.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.Id.ToString()
                                                 }).ToList();
            ViewBag.product = product;

            List<SelectListItem> current = (from x in _context.Currents.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.FirstName+" "+x.LastName,
                                              Value = x.Id.ToString()
                                          }).ToList();
            ViewBag.current = current;

            List<SelectListItem> employee = (from x in _context.Employees.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.FirstName + " " + x.LastName,
                                                Value = x.Id.ToString()
                                            }).ToList();
            ViewBag.employee = employee;
            return View();
        }

        // POST: SaleMovements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("create")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,EmployeeId,CurrentId,Amount,Price,TotalPrice")] SaleMovement saleMovement)
        {
            if (ModelState.IsValid)
            {
                saleMovement.Date = DateTime.Parse(DateTime.Now.ToShortDateString());

                _context.Add(saleMovement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleMovement);
        }

        // GET: SaleMovements/Edit/5
        [HttpGet, ActionName("edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<SelectListItem> product = (from x in _context.Products.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.Id.ToString()
                                                 }).ToList();
            ViewBag.product = product;

            List<SelectListItem> current = (from x in _context.Currents.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.FirstName+" "+x.LastName,
                                              Value = x.Id.ToString()
                                          }).ToList();
            ViewBag.current = current;

            List<SelectListItem> employee = (from x in _context.Employees.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.FirstName + " " + x.LastName,
                                                Value = x.Id.ToString()
                                            }).ToList();
            ViewBag.employee = employee;

            var saleMovement = await _context.SaleMovements.FindAsync(id);
            if (saleMovement == null)
            {
                return NotFound();
            }
            return View(saleMovement);
        }

        // POST: SaleMovements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("edit")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,EmployeeId,CurrentId,Amount,Price,TotalPrice")] SaleMovement saleMovement)
        {
            if (id != saleMovement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    saleMovement.Date = DateTime.Parse(DateTime.Now.ToShortDateString());

                    _context.Update(saleMovement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleMovementExists(saleMovement.Id))
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
            return View(saleMovement);
        }

        // GET: SaleMovements/Delete/5
        [HttpGet, ActionName("delete")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleMovement = await _context.SaleMovements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleMovement == null)
            {
                return NotFound();
            }

            return View(saleMovement);
        }

        // POST: SaleMovements/Delete/5
        [HttpPost, ActionName("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleMovement = await _context.SaleMovements.FindAsync(id);
            _context.SaleMovements.Remove(saleMovement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleMovementExists(int id)
        {
            return _context.SaleMovements.Any(e => e.Id == id);
        }
    }
}
