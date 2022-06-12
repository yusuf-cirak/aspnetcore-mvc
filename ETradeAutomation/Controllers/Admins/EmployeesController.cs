using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreMvcOnlineTicariOtomasyon.DataAccess.EntityFramework;
using CoreMvcOnlineTicariOtomasyon.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CoreMvcOnlineTicariOtomasyon.Controllers.Admins
{
    [Route("[controller]/[action]")]
    //[Authorize]
    [ControllerName("employees")]
    public class EmployeesController : Controller
    {
        private readonly ETradeAutomationContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string _folderPath;



        public EmployeesController(ETradeAutomationContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Employees
        [HttpGet, ActionName("index")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.Include(x => x.Department).ToListAsync());
        }

        [HttpGet, ActionName("list")]

        public async Task<IActionResult> List()
        {
            return View(await _context.Employees.Include(x=>x.Department).ToListAsync());
        }

        // GET: Employees/Details/5
        [HttpGet, ActionName("details")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create

        [HttpGet, ActionName("create")]
        public IActionResult Create()
        {
            List<SelectListItem> department = (from x in _context.Departments.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.Name,
                                              Value = x.Id.ToString()
                                          }).ToList();
            ViewBag.department = department;
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("create")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentId,FirstName,LastName,ImageUrl,Status,ImagePath")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _folderPath = Path.Combine(_hostEnvironment.WebRootPath, "images");
                if (!Directory.Exists(_folderPath))
                {
                    Directory.CreateDirectory(_folderPath); // Dosya yolu yoksa oluþtur
                }
                foreach (var item in employee.ImagePath)
                {
                    var fullFolderName = Path.Combine(_folderPath, item.FileName); // D://.../wwwroot/images/image.jpg
                                                                                  // file upload
                    using (var folderStream = new FileStream(fullFolderName, FileMode.Create))
                    {
                        await item.CopyToAsync(folderStream);
                    }

                    // product.ImagePath = product.ImageFile.FileName;
                    employee.Images.Add(new Image { ImagePath = item.FileName });
                }

                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        [HttpGet, ActionName("edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            List<SelectListItem> department = (from x in _context.Departments.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.Id.ToString()
                                                 }).ToList();
            ViewBag.department = department;
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.Include(y => y.Images).FirstOrDefaultAsync(y=>y.Id==id);
            if (employee == null)
            {
                return NotFound();
            }
            return View("edit",employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("edit")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentId,FirstName,LastName,ImageUrl,Status,ImagePath")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _folderPath = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    if (!Directory.Exists(_folderPath))
                    {
                        Directory.CreateDirectory(_folderPath); // Dosya yolu yoksa oluþtur
                    }
                    foreach (var item in employee.ImagePath)
                    {
                        var fullFolderName = Path.Combine(_folderPath, item.FileName); // D://.../wwwroot/images/image.jpg
                                                                                       // file upload
                        using (var folderStream = new FileStream(fullFolderName, FileMode.Create))
                        {
                            await item.CopyToAsync(folderStream);
                        }

                        // product.ImagePath = product.ImageFile.FileName;
                        employee.Images.Add(new Image { ImagePath = item.FileName });
                    }
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        [HttpGet, ActionName("delete")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var employee = await _context.Employees.FindAsync(id);
            foreach (var item in employee.Images)
            {
                System.IO.File.Delete(Path.Combine(_folderPath, item.ImagePath));
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }


        [ActionName("deleteimage")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.Images.FindAsync(id);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            System.IO.File.Delete(Path.Combine(_folderPath, image.ImagePath));
            return RedirectToAction(nameof(Edit), new { id = image.EmployeeId });
        }
    }
}
