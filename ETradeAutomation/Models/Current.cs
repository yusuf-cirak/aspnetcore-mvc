using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMvcOnlineTicariOtomasyon.Models
{
    public class Current // Cariler
    {
        public int Id { get; set; }
        [Column(TypeName = "NVarchar")]
        [StringLength(30, ErrorMessage = "Maksimum 30 karakter uzunlukta ad girebilirsiniz")]
        //[Required(ErrorMessage ="Bu alanı boş bırakamazsınız")]
        [Display(Name = "Adı")]

        public string FirstName { get; set; }
        [Column(TypeName = "NVarchar")]
        [StringLength(30, ErrorMessage = "Maksimum 30 karakter uzunlukta soyad girebilirsiniz")]
        //[Required(ErrorMessage = "Bu alanı boş bırakamazsınız")]
        [Display(Name = "Cari Soyadı")]


        public string LastName { get; set; }
        [Column(TypeName = "NVarchar")]
        [StringLength(13, ErrorMessage = "Maksimum 13 karakter uzunlukta şehir adı girebilirsiniz")]
        //[Required(ErrorMessage = "Bu alanı boş bırakamazsınız")]
        [Display(Name = "İkamet Şehri")]



        public string City { get; set; }

        [Column(TypeName = "NVarchar")]
        [StringLength(30, ErrorMessage = "Maksimum 30 karakter uzunlukta e-posta adresi girebilirsiniz")]
        //[Required(ErrorMessage = "Bu alanı boş bırakamazsınız")]
        [Display(Name = "E-posta Adresi")]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [Column(TypeName = "NVarchar")]
        [StringLength(50, ErrorMessage = "Maksimum 50 karakter uzunlukta şifre girebilirsiniz")]

        public string Password { get; set; }
        public ICollection<SaleMovement> SaleMovements { get; set; }
        [Display(Name = "Durum")]

        public bool Status { get; set; }


    }
}
