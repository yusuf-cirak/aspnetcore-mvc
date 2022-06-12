using CoreMvcOnlineTicariOtomasyon.DataAccess.EntityFramework;
using CoreMvcOnlineTicariOtomasyon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcOnlineTicariOtomasyon.Controllers.Currents
{
    [Route("[controller]/{action=panel}")]
    [Authorize]
    [ControllerName("current")]
    public class CurrentController : Controller
    {
        public readonly ETradeAutomationContext _context;

        public CurrentController(ETradeAutomationContext context)
        {
            _context = context;
        }

        [HttpGet("{id}"), ActionName("panel")] // routing'i current/panel/id şeklinde ayarlamak için ({id}) şeklinde kod yazdık.
        public IActionResult Panel(int id) // Current modelinin id'si bize loginden geldi. Modelin kendisi değil idsi geldiği için int id ile çalışıyoruz.
        {
            var value = _context.Currents.Where(x => x.Id == id);
            var sales = _context.SaleMovements.Where(x => x.CurrentId == id).Count();
            var totalPrice = _context.SaleMovements.Where(y => y.CurrentId == id).Sum(z => z.TotalPrice).ToString();
            var totalProduct = _context.SaleMovements.Where(x => x.CurrentId == id).Sum(y => y.Amount).ToString();
            var currentName = value.Select(x => x.FirstName + ' ' + x.LastName).FirstOrDefault();
            var email = value.Select(x => x.Email).FirstOrDefault();
            var messages = _context.Messages.Where(x => x.Receiver == email).ToList();

            var city = value.Select(x => x.City).FirstOrDefault();

            if (messages != null)
            {
                TempData["id"] = id; // giriş yaptıktan sonra sol menüyü kullanabilmek için _currentLayout'a id değerini temp data ile taşıyoruz.
                TempData["salesCount"] = sales;
                TempData["totalPrice"] = totalPrice;
                TempData["totalProduct"] = totalProduct;
                TempData["currentName"] = currentName;
                TempData["email"] = email;
                TempData["city"] = city;
                return View(messages);        // _currentLayout'ta model ile çalışmak yanlış. Eğer model ile çalışırsak diğer action'larda hep aynı modeli kullanmamız gerekecek.
            }
            return RedirectToAction("index", "auth");
        }

        [Authorize, HttpGet("{id}"), ActionName("orders")]
        public IActionResult Orders(int id)
        {
            var findCurrent = _context.Currents.Find(id);
            var mail = _context.Currents.FirstOrDefault(x => x.Email == findCurrent.Email);
            var values = _context.SaleMovements.Where(x => x.CurrentId == id)
                .Include(p => p.Product)
                .ToList();

            if (values != null)
            {
                TempData["id"] = findCurrent.Id;
                return View(values);
            }
            return RedirectToAction("panel", "current");
        }

        [HttpGet]
        public IActionResult Messages(int id)
        {
            var currentEmail = _context.Currents.Where(x => x.Id == id).Select(x => x.Email).FirstOrDefault();
            var messages = _context.Messages.Where(x => x.Receiver == currentEmail).OrderByDescending(y => y.Id).ToList();

            if (messages != null)
            {
                TempData["id"] = id;
                var countReceivedMessages = _context.Messages.Count(x => x.Receiver == currentEmail).ToString();
                ViewBag.countReceivedMessages = countReceivedMessages;

                var countSentMessages = _context.Messages.Count(x => x.Sender == currentEmail).ToString();
                ViewBag.countSentMessages = countSentMessages;
                return View(messages);

            }
            return RedirectToAction("panel", "current");
        }

        [HttpGet]
        public IActionResult SentMessages(int id)
        {
            var currentEmail = _context.Currents.Where(x => x.Id == id).Select(x => x.Email).FirstOrDefault();
            var messages = _context.Messages.Where(x => x.Sender == currentEmail).OrderByDescending(y => y.Id).ToList();

            if (messages != null)
            {
                TempData["id"] = id;
                var countSentMessages = _context.Messages.Count(x => x.Sender == currentEmail).ToString();
                ViewBag.countSentMessages = countSentMessages;

                var countReceivedMessages = _context.Messages.Count(x => x.Receiver == currentEmail).ToString();
                ViewBag.countReceivedMessages = countReceivedMessages;
                return View(messages);

            }
            return RedirectToAction("panel", "current");
        }


        [HttpGet]
        public IActionResult MessageDetail(int messageId)
        {
            var value = _context.Messages.Where(x => x.Id == messageId);

            var findReceiverEmail = value.Select(y => y.Receiver).FirstOrDefault();
            var findSenderEmail = value.Select(y => y.Sender).FirstOrDefault();

            var findIdFromEmail = _context.Currents.Where(z => z.Email == findReceiverEmail).Select(a => a.Id).FirstOrDefault();

            TempData["id"] = findIdFromEmail;


            var countSentMessages = _context.Messages.Count(x => x.Sender == findSenderEmail).ToString();
            ViewBag.countSentMessages = countSentMessages;

            var countReceivedMessages = _context.Messages.Count(x => x.Receiver == findReceiverEmail).ToString();
            ViewBag.countReceivedMessages = countReceivedMessages;
            return View(value);
        }


        [HttpGet]
        public IActionResult SendMessage(int id)
        {
            TempData["id"] = id;
            var currentEmail = _context.Currents.Where(x => x.Id == id).Select(x => x.Email).FirstOrDefault();

            var messageId = _context.Messages.Where(y => y.Receiver == currentEmail).Select(z => z.Id).FirstOrDefault();

            var sender = _context.Messages.Where(x => x.Id == messageId).Select(x => x.Sender).FirstOrDefault();

            var countSentMessages = _context.Messages.Count(x => x.Sender == sender).ToString();
            ViewBag.countSentMessages = countSentMessages;

            var countReceivedMessages = _context.Messages.Count(x => x.Receiver == currentEmail).ToString();
            ViewBag.countReceivedMessages = countReceivedMessages;
            TempData["eMail"] = currentEmail;

            return View();
        }

        [HttpPost, ActionName("sendmessage")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage([Bind("Sender,Receiver,Subject,Description")] Message message) // Overposting ataklardan korunmak için
        {

            if (ModelState.IsValid) // Sunucu taraflı doğrulama
            {
                message.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        public async Task<IActionResult> CargoDetails(int id, string filterText)
        {
            TempData["id"] = id;
            if (!String.IsNullOrEmpty(filterText))
            {
                
                //var receiver = _context.Currents.Where(x => x.Id == id).Select(y => y.FirstName + ' ' + y.LastName).FirstOrDefault();

                //var filter = _context.CargoDetails.Where(x => x.Receiver == "deneme ad deneme soyad").Where(y => y.TrackNumber.Contains(filterText.ToUpper()));
                var filter = _context.CargoDetails.Where(x => x.TrackNumber.Contains(filterText.ToUpper()));
                return View(filter);
            }
            await Task.Yield();
            return View();
        }

        public async Task<IActionResult> CargoTrack(string trackNumber)
        {
            if (!String.IsNullOrEmpty(trackNumber))
            {
                var filter = _context.CargoTracks.Where(x => x.TrackNumber == trackNumber).ToList();
                return View(filter);

            }
            await Task.Yield();
            return View();
        }

        //[HttpGet, ActionName("usersettings")]
        //public async Task<PartialViewResult> UserSettings(int id)
        //{
        //    var values = _context.Currents.Find(id);
        //    await Task.Yield();
        //    return PartialView("usersettings",id);
        //}

        //[HttpPost, ActionName("usersettings")]

        //[ValidateAntiForgeryToken]
        //public async Task<PartialViewResult> UserSettings([Bind("City,Email,Password")] Current current)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Update(current);
        //        await _context.SaveChangesAsync();
        //        return PartialView(nameof(Index));
        //    }
        //    return PartialView(nameof(Index));
        //}

        [HttpGet,ActionName("announcements")]
        public PartialViewResult Announcements()
        {
            var values = _context.Messages.Where(x => x.Sender == "admin").ToList();
            return PartialView(values);
        }
    }

}


        

