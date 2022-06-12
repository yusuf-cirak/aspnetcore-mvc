using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoreMvcOnlineTicariOtomasyon.DataAccess.EntityFramework;
using CoreMvcOnlineTicariOtomasyon.Models;
using CoreMvcOnlineTicariOtomasyon.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreMvcOnlineTicariOtomasyon.Controllers.Admins
{
    [Route("[controller]/[action]")]
    //[Authorize]
    [ControllerName("products")]
    public class ProductsController : Controller
    {
        private readonly ETradeAutomationContext _context;


        public ProductsController(ETradeAutomationContext context)
        {
            _context = context; // Veritabanı
        }

        // GET: Product
        [HttpGet, ActionName("index")]

        public async Task<IActionResult> Index(string filterText)
        {
            if (!String.IsNullOrEmpty(filterText))
            {
                var filter = _context.Products.Include(y=>y.Category).Where(x => x.Name.ToLower().Contains(filterText.ToLower()));
                return View(filter);
            }
            return View(await _context.Products.Where(x => x.Status == true).Include(y=>y.Category).ToListAsync());
        }

        [HttpGet, ActionName("details")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(x=>x.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Details/5
        [HttpGet, ActionName("update")]

        public IActionResult Update(int id)
        {
            List<SelectListItem> value = (from x in _context.Categories.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.Name,
                                              Value = x.Id.ToString()
                                          }).ToList();
            ViewBag.value = value;
            var getProduct = _context.Products.Include(y=>y.Category).SingleOrDefault(x=>x.Id==id);
            return View("update", getProduct);
        }
        [HttpPost, ActionName("update")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,CategoryId,Name,BrandName,Stock,UnitBuyPrice,UnitSellPrice,Image,Status")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Product/Create
        [HttpGet, ActionName("add")]

        public IActionResult Add()
        {
            List<SelectListItem> value = (from x in _context.Categories.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.Name,
                                              Value = x.Id.ToString()
                                          }).ToList();
            ViewBag.value = value;
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost, ActionName("add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Name,BrandName,Stock,UnitBuyPrice,UnitSellPrice,Image,Status,CategoryId")] Product product) // Overposting ataklardan korunmak için
        {
            if (ModelState.IsValid) // Sunucu taraflı doğrulama
            {
                product.Status = true;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }



        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            await Task.Yield();
            return RedirectToAction(nameof(Index));
        }


        // biz dün sell metodunu ayarlamıştık değil mi? evet hocam delete ile ilgili de sadece modalı çalıştırdık düzgün şekilde
        [HttpGet,ActionName("sell")]
        public IActionResult Sell(int id)
        {
            SellProductViewModel sellProductViewModel = new SellProductViewModel();
            sellProductViewModel.Currents = (from x in _context.Currents.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.FirstName + " " + x.LastName,
                                                Value = x.Id.ToString()
                                            }).ToList();

            sellProductViewModel.Employees = (from x in _context.Employees.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.FirstName + " " + x.LastName,
                                                 Value = x.Id.ToString()
                                             }).ToList();


            var products = _context.Products.Find(id);
            sellProductViewModel.Product = products;
            sellProductViewModel.ProductId = id;
            sellProductViewModel.Price = products.UnitSellPrice;



            return View(sellProductViewModel);
        }

        [HttpPost, ActionName("sell")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sell([Bind("ProductId,CurrentId,EmployeeId,Amount,Price,TotalPrice")] SaleMovement saleMovement) // Overposting ataklardan korunmak için
        {
            if (ModelState.IsValid) // Sunucu taraflı doğrulama
            {
                saleMovement.Date = DateTime.Parse(DateTime.Now.ToShortDateString());

                _context.Add(saleMovement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleMovement);
        }



        public async Task<IActionResult> List()
        {
        
        var values=await _context.Products.Where(x=>x.Status==true).Include(y=>y.Category).ToListAsync();
            return View(values);
        }
        

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}