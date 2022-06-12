using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ETradeErkanHurnalıP2.Data;
using ETradeErkanHurnalıP2.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ETradeErkanHurnalıP2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ETradeContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string _folderPath;

        public ProductsController(ETradeContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.Include(x => x.Images).ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(x => x.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ImageFile")] Product product)
        {
            if (ModelState.IsValid)
            {
                 _folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");// ../wwwroot/+ images
                if (!Directory.Exists(_folderPath))
                {
                    Directory.CreateDirectory(_folderPath);
                }

                foreach (var item in product.ImageFile)
                {
                    var fullFolderName = Path.Combine(_folderPath, item.FileName);
                    using (var folderStream = new FileStream(fullFolderName, FileMode.Create))
                    {
                        await item.CopyToAsync(folderStream);
                    }
                    product.Images.Add(new Image { ImagePath = item.FileName });
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(y => y.Images).SingleOrDefaultAsync(y => y.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ImageFile")] Product product)
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

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.Include(x=>x.Images).SingleOrDefaultAsync(x=>x.Id==id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            foreach (var item in product.Images)
            {
                System.IO.File.Delete(Path.Combine(_folderPath,item.ImagePath));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        [ActionName("deleteimage")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.Images.FindAsync(id);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            System.IO.File.Delete(Path.Combine(_folderPath,image.ImagePath));
            return RedirectToAction(nameof(Edit), new { id = image.ProductId });
        }
    }
}
