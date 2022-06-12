using Microsoft.AspNetCore.Mvc;
using QRCoder; // qr file missing
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcOnlineTicariOtomasyon.Controllers.Admins
{
    public class QRController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Index(string code)
        {
            using (MemoryStream memoryStream=new MemoryStream())
            {
                QRCodeGenerator codeGenerator = new QRCodeGenerator();
                QRCodeGenerator.QRCode qRCode = codeGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                using (Bitmap image=qRCode.GetGraphic(10))
                {
                    image.Save(memoryStream, ImageFormat.Png);
                    ViewBag.qrCodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return View();
        }
    }
}
