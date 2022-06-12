using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcOnlineTicariOtomasyon.Models
{
    public class CargoDetail
    {
        public int Id { get; set; }



        [Column(TypeName = "nvarchar")]
        [StringLength(200)]
        [Display(Name ="Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Takip numarası")]
        [Column(TypeName = "nvarchar")]
        [StringLength(10)]
        public string TrackNumber { get; set; }



        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [Display(Name = "Personel")]
        public string Employee { get; set; }
        [Display(Name = "Alıcı")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]

        public string Receiver { get; set; }
        [Display(Name = "Tarih")]

        public DateTime Date { get; set; }
    }
}
