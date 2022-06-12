using System.Linq;
using System.Threading.Tasks;
using CoreMvcOnlineTicariOtomasyon.DataAccess.EntityFramework;
using CoreMvcOnlineTicariOtomasyon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Mvc.Core;


namespace CoreMvcOnlineTicariOtomasyon.Controllers.Admins
{
    [Route("[controller]/[action]")]
    //[Authorize]
    [ControllerName("categories")]
    public class CategoriesController:Controller
    {
        private readonly ETradeAutomationContext _context;


        public CategoriesController(ETradeAutomationContext context)
        {
            _context = context; // Veritabanı
        }


        [HttpGet, ActionName("index")]
        public IActionResult Index(int page=1)
        {
            var result = _context.Categories.ToList().ToPagedList(page, 4);
            return View(result);
        }


        [HttpGet, ActionName("add")]

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost, ActionName("add")]

        public IActionResult Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("index");
        }


        [HttpGet, ActionName("update")]

        public IActionResult Update(int id)
        {
            var getCategory=_context.Categories.Find(id);
            return View("update",getCategory);
        }

        [HttpPost, ActionName("update")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }


        [HttpGet, ActionName("delete")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }    
    }
}