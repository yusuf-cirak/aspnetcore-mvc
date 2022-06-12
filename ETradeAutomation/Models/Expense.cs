using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMvcOnlineTicariOtomasyon.Models
{
    public class Expense // Giderler
    {
        public int Id { get; set; }

        [Column(TypeName = "NVarchar")]
        [StringLength(100)]
        [Display(Name = "Açıklama")]

        public string Description { get; set; }
        [Display(Name = "Tarih")]

        public DateTime Date { get; set; }
        [Display(Name = "Maliyet")]

        public decimal Cost { get; set; } // gider maliyeti

    }
}
