using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcOnlineTicariOtomasyon.Models
{
    public class CargoTrack
    {
        public int Id { get; set; }
        [Display(Name ="Takip numarası")]
        [Column(TypeName = "nvarchar")]
        [StringLength(10)]
        public string TrackNumber { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(200)]

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }
    }
}
