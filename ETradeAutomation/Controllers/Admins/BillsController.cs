using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreMvcOnlineTicariOtomasyon.DataAccess.EntityFramework;
using CoreMvcOnlineTicariOtomasyon.Models;
using Microsoft.AspNetCore.Authorization;

namespace CoreMvcOnlineTicariOtomasyon.Controllers.Admins
{
    [Route("[controller]/[action]")]
    //[Authorize]
    [ControllerName("bills")]
    public class BillsController : Controller
    {
        private readonly ETradeAutomationContext _context;

        public BillsController(ETradeAutomationContext context)
        {
            _context = context;
        }

        // GET: Bills
        [HttpGet, ActionName("index")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.Bills.ToListAsync());
        }

        // GET: Bills/Details/5
        [HttpGet, ActionName("details")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.BillPens
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create
        [HttpGet, ActionName("create")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SerialNumber,LineNumber,Date,TaxAdministration,Hour,DeliveryPerson,ReceiverPerson,TotalPrice")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                bill.Date = DateTime.Parse(DateTime.Now.ToShortDateString());

                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bill);
        }

        // GET: Bills/Edit/5
        [HttpGet, ActionName("edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SerialNumber,LineNumber,TaxAdministration,Hour,DeliveryPerson,ReceiverPerson,TotalPrice")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                bill.Date = DateTime.Parse(DateTime.Now.ToShortDateString());

                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
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
            return View(bill);
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }

        [HttpGet,ActionName("create-bill-pen")]
        public IActionResult CreateBillPen()
        {
                List<SelectListItem> bill = (from x in _context.Bills.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Id.ToString(),
                                                     Value = x.Id.ToString()
                                                 }).ToList();
            ViewBag.bill = bill;
            return View();
        }

        [HttpPost, ActionName("create-bill-pen")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBillPen([Bind("Id,Description,Amount,QuantityPerUnit,Price,BillId")] BillPen billPen)
        {
            if (ModelState.IsValid)
            {
                //billPen.Date = DateTime.Parse(DateTime.Now.ToShortDateString());

                _context.Add(billPen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billPen);
        }
    }
}
