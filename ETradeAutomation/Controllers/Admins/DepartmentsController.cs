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
    [ControllerName("departments")]
    public class DepartmentsController : Controller
    {
        private readonly ETradeAutomationContext _context;

        public DepartmentsController(ETradeAutomationContext context)
        {
            _context = context;
        }

        // GET: Departments
        [HttpGet, ActionName("index")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
        }

        // GET: Departments/Details/5
        [HttpGet, ActionName("details")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                var department = await _context.Departments.Where(x => x.Id == id).Select(y => y.Name).FirstOrDefaultAsync();
            ViewBag.department = department;
            var values = _context.Employees.Where(x => x.Department.Id == id).ToList();
            return View(values);
        }

        // GET: Departments/Create
        [HttpGet, ActionName("create")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost, ActionName("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        [HttpGet, ActionName("edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("edit")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        [HttpGet, ActionName("delete")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet, ActionName("sales")]

        public IActionResult Sales(int id)
        {
            var values = _context.SaleMovements.Where(x => x.EmployeeId == id).Include(y=>y.Product).Include(z=>z.Current).ToList();
            var employee = _context.Employees.Where(x => x.Id == id).Select(y => y.FirstName + " " + y.LastName).FirstOrDefault();
            ViewBag.employee = employee;
            return View(values);
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}
