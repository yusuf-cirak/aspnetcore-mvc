using CoreMvcOnlineTicariOtomasyon.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcOnlineTicariOtomasyon.Controllers.Admins
{
    [Route("[controller]/[action]")]
    //[Authorize]
    [ControllerName("todolist")]
    public class ToDoListsController : Controller
    {


        private readonly ETradeAutomationContext _context;

        public ToDoListsController(ETradeAutomationContext context)
        {
            _context = context;
        }
        [HttpGet, ActionName("index")]

        public IActionResult Index()
        {
            var countCurrents = _context.Currents.Count().ToString();
            ViewBag.countCurrents = countCurrents;

            var countProducts = _context.Products.Count().ToString();
            ViewBag.countProducts = countProducts;

            var countCategories = _context.Categories.Count().ToString();
            ViewBag.countCategories = countCategories;

            var countCurrentCities = (from x in _context.Currents select x.City).Distinct().Count().ToString();
            ViewBag.countCurrentCities = countCurrentCities;


            var toDoLists = _context.ToDoLists.ToList();

            return View(toDoLists);
        }
    }
}
