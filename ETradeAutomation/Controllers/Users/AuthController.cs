using CoreMvcOnlineTicariOtomasyon.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using CoreMvcOnlineTicariOtomasyon.Models;

namespace CoreMvcOnlineTicariOtomasyon.Controllers.Users
{
    [Route("[controller]/{action=Index}")]

    [ControllerName("auth")]
    public class AuthController : Controller
    {
        public readonly ETradeAutomationContext _context;

        public AuthController(ETradeAutomationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet,ActionName("register")]
        public async Task<PartialViewResult> Register()
        {
            await Task.Yield();
            return PartialView();
        }

        [HttpPost,ActionName("register")]
        [ValidateAntiForgeryToken]
        /*Id*/
        public async Task<PartialViewResult> Register([Bind("FirstName,LastName,City,Email,Password")] Current current) // Status gelmeyecek
        {
            if (ModelState.IsValid)
            {
                _context.Add(current);
                await _context.SaveChangesAsync();
                return PartialView(nameof(Index));
            }
            return PartialView(nameof(Index));
        }



        [AllowAnonymous]
        [HttpGet, ActionName("login")]
        public async Task<PartialViewResult> Login()
        {
            await Task.Yield();
            return PartialView();
        }


        [AllowAnonymous]
        [HttpPost, ActionName("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] Current current)
        {

            if (ModelState.IsValid)
            {
                var result = _context.Currents.FirstOrDefault(x => x.Email == current.Email && x.Password == current.Password);

                if (result != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email,current.Email)
                };

                    var userIdentity = new ClaimsIdentity(claims, "login");

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction("panel", "current",
                        new {id= result.Id}); // login olduktan sonra current panel'e cari bilgilerini göndermek, linki de current/user/id şeklinde ayarlamak için
                                              // bu kodu yazdık.
                }

            }         
            await Task.Yield();
            return PartialView(nameof(Index));
        }

        [HttpGet, ActionName("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "auth");
        }


        [AllowAnonymous]
        [HttpGet, ActionName("adminlogin")]
        public async Task<PartialViewResult> AdminLogin()
        {
            await Task.Yield();
            return PartialView();
        }


        [AllowAnonymous]
        [HttpPost, ActionName("adminlogin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin([Bind("UserName,Password")] Admin admin)
        {

            if (ModelState.IsValid)
            {
                var result = _context.Admins.FirstOrDefault(x => x.UserName == admin.UserName && x.Password == admin.Password);

                if (result != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,admin.UserName)
                };

                    var userIdentity = new ClaimsIdentity(claims, "login");

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction("index", "home");
                }

            }
            await Task.Yield();
            return PartialView(nameof(Index));
        }


    }
}
