using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMvcOnlineTicariOtomasyon.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Display(Name = "Departman")]

        public int DepartmentId { get; set; }

        [Column(TypeName = "NVarchar")]
        [StringLength(30)]
        [Display(Name = "Adı")]

        public string FirstName { get; set; }

        [Column(TypeName = "NVarchar")]
        [StringLength(30)]
        [Display(Name = "Soyadı")]

        public string LastName { get; set; }

        [Column(TypeName = "NVarchar")]
        [StringLength(250)]
        [Display(Name = "Fotoğraf (Link)")]

        public string ImageUrl { get; set; }
        public virtual Department Department { get; set; }
        public ICollection<SaleMovement> SaleMovements { get; set; }
        [Display(Name = "Durum")]

        public bool Status { get; set; }

        [Display(Name = "Resimler")]

        public List<Image> Images { get; set; } = new List<Image>(); // Null reference hatası almamak için yazdığımız kod.

        [Display(Name = "Fotoğraf (Dosya)")]
        [NotMapped] // Veritabanına yansımamasını sağlayacak kod.
        public IFormFile[] ImagePath { get; set; }


    }
}
