using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMvcOnlineTicariOtomasyon.Models
{
    public class Category
    { //CRUD işlemleri = Listeleme, ekleme , silme , güncelleme
        public int Id { get; set; }

        [Column(TypeName = "NVarchar")]
        [StringLength(30)]
        [Display(Name = "Kategori Adı")]

        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
