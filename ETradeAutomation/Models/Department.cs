using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMvcOnlineTicariOtomasyon.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Column(TypeName = "NVarchar")]
        [StringLength(30)]
        [Display(Name = "Departman Adı")]


        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
        [Display(Name = "Durum")]

        public bool Status { get; set; }
    }
}
