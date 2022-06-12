using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMvcOnlineTicariOtomasyon.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Column(TypeName = "NVarChar")] // Veritabanında fazla yer kaplamaması için yazdığımız kod. 
        [StringLength(30)]
        [Display(Name = "Ürün adı")]

        public string Name { get; set; }

        [Column(TypeName = "NVarChar")]
        [StringLength(30)]
        [Display(Name = "Marka Adı")]

        public string BrandName { get; set; }
        [Display(Name = "Stok adedi")]

        public short Stock { get; set; } // short'un sql'de karşılığı 
        [Display(Name = "Alış fiyatı")]

        public decimal UnitBuyPrice { get; set; }
        [Display(Name = "Satış fiyatı")]

        public decimal UnitSellPrice { get; set; }


        [Display(Name = "Açıklama")]
        [Column(TypeName = "NVarChar")]
        [StringLength(2000)]
        public string Description { get; set; }

        [Column(TypeName = "NVarChar")]
        [StringLength(250)]
        [Display(Name = "Ürün resmi")]

        public string Image { get; set; }
        [Display(Name = "Durum")]

        public bool Status { get; set; }
        [Display(Name = "Kategori Adı")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } // Virtual keywordü sayesinde ürünleri getirirken kategorinin bütün özelliklerine ulaşabiliyoruz.

        public ICollection<SaleMovement> SaleMovements { get; set; }


    }
}
