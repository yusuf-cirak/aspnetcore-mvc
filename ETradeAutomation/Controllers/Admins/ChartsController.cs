using CoreMvcOnlineTicariOtomasyon.DataAccess.EntityFramework;
using CoreMvcOnlineTicariOtomasyon.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcOnlineTicariOtomasyon.Controllers.Admins
{
    public class ChartsController : Controller
    {
        public readonly ETradeAutomationContext _context;

        public ChartsController(ETradeAutomationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return Json(ProductList());
        }

        public List<Product> ProductList()
        {
            List<Product> products = new List<Product>();
            products = _context.Products.Select(x => new Product
            {
                Name = x.Name,
                Stock = x.Stock
            }).ToList();

            return products;
        }
    }
}
