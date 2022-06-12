using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMvcOnlineTicariOtomasyon.DataAccess.EntityFramework;
using CoreMvcOnlineTicariOtomasyon.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
//using CoreMvcOnlineTicariOtomasyon.Models;

namespace CoreMvcOnlineTicariOtomasyon.Controllers.Admins
{
    [Route("[controller]/[action]")]
    //[Authorize]
    [ControllerName("statics")]

    public class StaticsController : Controller
    {
        private readonly ETradeAutomationContext _context;

        public StaticsController(ETradeAutomationContext context)
        {
            _context = context;
        }
        [HttpGet, ActionName("index")]

        public async Task<IActionResult> Index()
        {
            var currents = _context.Currents.Count().ToString();
            ViewBag.currents = currents;

            var products = _context.Products.Count().ToString();
            ViewBag.products = products;

            var employees = _context.Employees.Count().ToString();
            ViewBag.employees = employees;

            var categories = _context.Categories.Count().ToString();
            ViewBag.categories = categories;

            var stocks = _context.Products.Sum(x => x.Stock).ToString();
            ViewBag.stocks = stocks;

            var criticals = _context.Products.Count(x => x.Stock <= 20).ToString();
            ViewBag.criticals = criticals;

            var brands = (from x in _context.Products select x.BrandName).Distinct().Count().ToString();
            ViewBag.brands = brands;

            var maxPrice = (from x in _context.Products orderby x.UnitSellPrice descending select x.Name).FirstOrDefault();
            ViewBag.maxPrice = maxPrice;

            var minPrice = (from x in _context.Products orderby x.UnitSellPrice ascending select x.Name).FirstOrDefault();
            ViewBag.minPrice = minPrice;

            var television = _context.Products.Count(x => x.Name == "4K Televizyon").ToString();
            ViewBag.television = television;

            var phone = _context.Products.Count(x => x.Name == "Iphone 12").ToString();
            ViewBag.phone = phone;

            var totalSalePrice = _context.SaleMovements.Sum(x => x.TotalPrice).ToString();
            ViewBag.totalSalePrice = totalSalePrice;


            var todaySalesCount = _context.SaleMovements.Count(x => x.Date == DateTime.Today).ToString();
            ViewBag.todaySalesCount = todaySalesCount;

            var todaySalesTotalPrice = _context.SaleMovements.Where(x => x.Date == DateTime.Today).Sum(y => y.TotalPrice).ToString();
            ViewBag.todaySalesTotalPrice = todaySalesTotalPrice;


            var maxBrand = _context.Products.GroupBy(x => x.BrandName).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.maxBrand = maxBrand;

            var mostSoldProduct = _context.Products.Where
            (x => x.Id == _context.SaleMovements.GroupBy(x => x.ProductId).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())
            .Select(k => k.Name).FirstOrDefault();
            ViewBag.mostSoldProduct = mostSoldProduct;

            var nullableTodaySalesTotalPrice = _context.SaleMovements.Where(x => x.Date == DateTime.Today).Sum(y => (decimal?)y.TotalPrice).ToString();
            ViewBag.nullableTodaySalesTotalPrice = nullableTodaySalesTotalPrice;

            await Task.Yield();
            return View();
        }
        [HttpGet, ActionName("simpletable")]

        public async Task<IActionResult> SimpleTable()
        {
            var employees = from x in _context.Employees
                            group x by x.Department.Name into g
                            select new EmployeesDepartmentDto
                            {
                                Department = g.Key,
                                Count = g.Count()
                            };

            var currentCity = from x in _context.Currents
                              group x by x.City into g
                              select new CurrentCityDto
                              {
                                  City = g.Key,
                                  Count = g.Count()
                              };

            var currents = _context.Currents.ToList();
            var products = _context.Products.ToList();

            var brands = from x in _context.Products
                         group x by x.BrandName into g
                         select new BrandsDto
                         {
                             Brand = g.Key,
                             Count = g.Count()
                         };


            await Task.Yield();

            StaticViewModel model = new StaticViewModel
            {
                CurrentCityDtos = currentCity.ToList(),
                EmployeesDepartmentDtos = employees.ToList(),
                Currents = currents,
                Products = products,
                BrandsDtos=brands.ToList()
            };

            return View(model);
        }


        // [ChildActionOnly]

        [HttpGet, ActionName("employeespartial")]
        public async Task<PartialViewResult> EmployeesPartial()
        {
            var values = from x in _context.Employees
                         group x by x.Department.Name into g
                         select new EmployeesDepartmentDto
                         {
                             Department = g.Key,
                             Count = g.Count()
                         };
            await Task.Yield();
            return PartialView(values.ToList());
        }


        [HttpGet, ActionName("currentspartial")]
        public async Task<PartialViewResult> CurrentsPartial()
        {
            var values = _context.Currents.ToList();
            await Task.Yield();
            return PartialView(values);
        }

        [HttpGet, ActionName("productspartial")]
        public async Task<PartialViewResult> ProductsPartial()
        {
            var values = _context.Products.ToList();
            await Task.Yield();
            return PartialView(values);
        }



        [HttpGet, ActionName("brandspartial")]
        public async Task<PartialViewResult> BrandsPartial()
        {
            var brands = from x in _context.Products
                         group x by x.BrandName into g
                         select new BrandsDto
                         {
                             Brand = g.Key,
                             Count = g.Count()
                         };
            await Task.Yield();
            return PartialView(brands.ToList());
        }



    }
}