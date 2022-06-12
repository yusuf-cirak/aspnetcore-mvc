using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcOnlineTicariOtomasyon.Models

{
    public class Admin
    {
        public int Id { get; set; }
        [Column(TypeName = "NVarchar")]
        [StringLength(20)]
        [Display(Name = "Kullanıcı adı")]

        public string UserName { get; set; }

        [Column(TypeName = "NVarchar")]
        [StringLength(30)]
        [Display(Name = "Şifre")]


        public string Password { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(1)]
        [Display(Name = "Yetki")]


        public string Authority { get; set; } // yetki
    }
}
