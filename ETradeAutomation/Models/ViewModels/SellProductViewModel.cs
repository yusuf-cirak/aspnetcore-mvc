using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcOnlineTicariOtomasyon.Models.ViewModels
{
    public class SellProductViewModel
    {
        public List<SelectListItem> Currents { get; set; }

        public List<SelectListItem> Employees { get; set; }

        public Product Product { get; set; }
        public int Id { get; set; }
        [Display(Name = "Ürün")]

        public int ProductId { get; set; }
        [Display(Name = "Cari")]

        public int CurrentId { get; set; }
        [Display(Name = "Personel")]

        public int EmployeeId { get; set; }

        // ürün => product
        // cari => current
        // personel => employee
        [Display(Name = "Satış tarihi")]

        public DateTime Date { get; set; } = DateTime.Now;
        [Display(Name = "Satış adeti")]

        public int? Amount { get; set; } // miktar
        [Display(Name = "Satış fiyatı")]

        public decimal? Price { get; set; } // fiyat
        [Display(Name = "Satış toplam fiyatı")]

        public decimal? TotalPrice { get; set; } // toplam fiyat
    }
}
