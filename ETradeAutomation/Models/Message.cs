using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcOnlineTicariOtomasyon.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Display(Name ="Mesajı gönderen kişi")]
        [Column(TypeName ="nvarchar")]
        [StringLength(50)]
        public string Sender { get; set; } // Mesajı gönderen
        [Display(Name = "Mesajı alan kişi")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Receiver { get; set; } // Mesajı alan
        [Display(Name = "Konu")]
        [Column(TypeName = "nvarchar")]
        [StringLength(30)]
        public string Subject { get; set; } // Konu
        [Display(Name = "Açıklama")]
        [Column(TypeName = "nvarchar")]
        [StringLength(2000)]
        public string Description { get; set; } // Açıklama
        public DateTime Date { get; set; }
    }
}
