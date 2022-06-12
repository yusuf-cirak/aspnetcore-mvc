using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMvcOnlineTicariOtomasyon.Models
{
    public class SaleMovement // satış hareketleri
    {
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

        public DateTime Date { get; set; }
        [Display(Name = "Satış miktarı")]

        public int Amount { get; set; } // miktar
        [Display(Name = "Satış fiyatı")]

        public decimal Price { get; set; } // fiyat
        [Display(Name = "Satış toplam fiyatı")]

        public decimal TotalPrice { get; set; } // toplam fiyat
        public virtual Product Product { get; set; }
        public virtual Current Current { get; set; }

        public virtual Employee Employee { get; set; }


    }
}
