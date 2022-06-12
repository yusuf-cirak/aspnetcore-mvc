using CoreMvcOnlineTicariOtomasyon.DataAccess.EntityFramework;
using CoreMvcOnlineTicariOtomasyon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcOnlineTicariOtomasyon.Controllers.Admins
{
    public class CargoController : Controller
    {

        public readonly ETradeAutomationContext _context;

        public CargoController(ETradeAutomationContext context)
        {
            _context = context;
        }

        public IActionResult Index(string filterText)
        {
            if (!String.IsNullOrEmpty(filterText))
            {
                var filter = _context.CargoDetails.Where(x => x.TrackNumber.Contains(filterText.ToUpper()));
                return View(filter);
            }
            var cargos = _context.CargoDetails.ToList();
            return View(cargos);
        }


        [HttpGet]
        public IActionResult Add()
        {
            Random random = new Random();
            string[] characters = { "A", "B", "C", "D", "E","F","G","H","K","L","M","N"};
            int c1, c2, c3;
            c1 = random.Next(0, characters.Length);
            c2 = random.Next(0, characters.Length);
            c3 = random.Next(0, characters.Length);

            int n1, n2, n3;
            n1 = random.Next(100, 1000);
            n2 = random.Next(10, 99);
            n3 = random.Next(10, 99);

            string trackNumber = n1.ToString() + characters[c1] + n2 + characters[c2] + n3 + characters[c3];
            ViewBag.trackNumber = trackNumber;
            return View();
        }

        [HttpPost, ActionName("add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Description,TrackNumber,Employee,Receiver")] CargoDetail cargoDetail) // Overposting ataklardan korunmak için
        {
            if (ModelState.IsValid) // Sunucu taraflı doğrulama
            {
                cargoDetail.Date = DateTime.Parse(DateTime.Now.ToShortDateString());

                _context.Add(cargoDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cargoDetail);
        }


        public async Task<IActionResult> Track(string trackNumber)
        {
            if (!String.IsNullOrEmpty(trackNumber))
            {
                var filter = _context.CargoTracks.Where(x => x.TrackNumber == trackNumber).ToList();
                return View(filter);

            }
            await Task.Yield();
            return View();

        }

    }
}
