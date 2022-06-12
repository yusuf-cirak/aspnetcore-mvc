using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMvcOnlineTicariOtomasyon.Models
{
    public class BillPen // Fatura kalemi
    {
        public int Id { get; set; }


        [Column(TypeName = "NVarchar")]
        [StringLength(100)]
        [Display(Name = "Fatura açıklama")]
        public string Description { get; set; } // fatura açıklama
        [Display(Name = "Miktar")]

        public int Amount { get; set; } // miktar
        [Display(Name = "Birim fiyat")]
        public short QuantityPerUnit { get; set; } // birim miktarı
        [Display(Name = "Fiyat")]

        public decimal Price { get; set; } // Fiyat
        [Display(Name ="Fatura ID")]
        public int BillId { get; set; }
        public virtual Bill Bill { get; set; } // fatura kalemlerinin bir tane faturası olur
    }
}
