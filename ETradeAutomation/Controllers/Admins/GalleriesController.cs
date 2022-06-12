using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMvcOnlineTicariOtomasyon.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
//using CoreMvcOnlineTicariOtomasyon.Models;

namespace CoreMvcOnlineTicariOtomasyon.Controllers.Admins
{
    [Route("[controller]/[action]")]
    //[Authorize]
    [ControllerName("galleries")]
    public class GalleriesController : Controller
    {
        private readonly ETradeAutomationContext _context;
        public GalleriesController(ETradeAutomationContext context)
        {
            _context=context;
        }
        [HttpGet, ActionName("index")]

        public async Task<IActionResult> Index()
        {
            var values=_context.Products.ToList();
            await Task.Yield();

            return View(values);
        }
    }
}