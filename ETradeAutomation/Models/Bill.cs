using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMvcOnlineTicariOtomasyon.Models
{
    public class Bill //fatura
    {
        public int Id { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(1)]
        [Display(Name = "Fatura seri no")]

        public string SerialNumber { get; set; } // fatura seri no

        [Column(TypeName = "Varchar")]
        [StringLength(6)]
        [Display(Name = "Fatura sıra no")]

        public string LineNumber { get; set; } // fatura sıra no
        [Display(Name = "Fatura tarihi")]

        public DateTime Date { get; set; } // fatura tarihi

        [Column(TypeName = "NVarchar")]
        [StringLength(60)]
        [Display(Name = "Vergi dairesi")]

        public string TaxAdministration { get; set; } // vergi dairesi

        [Display(Name = "Fatura saati")]
        public string Hour { get; set; } // fatura saati


        [Column(TypeName = "NVarchar")]
        [StringLength(30)]
        [Display(Name = "Fatura teslim eden kişi")]
        public string DeliveryPerson { get; set; } // faturayı teslim eden kişi


        [Column(TypeName = "NVarchar")]
        [StringLength(30)]
        [Display(Name = "Fatura teslim alan kişi")]
        public string ReceiverPerson { get; set; } // faturayı teslim alan kişi

        [Display(Name = "Toplam Tutar")]
        public decimal TotalPrice { get; set; } // toplam fatura tutarı
        public ICollection<BillPen> BillPens { get; set; } // bir faturanın birden fazla fatura kalemi olabilir
    }
}
