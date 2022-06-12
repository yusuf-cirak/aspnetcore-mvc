using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ETradeErkanHurnalıP2.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name ="İsmi")]
        [Required(ErrorMessage ="{0} alanı gereklidir.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="{0} alanı gereklidir.")]
        [Display(Name ="Açıklaması")]
        public string Description { get; set; }


        [Display(Name ="Fiyatı")]
        [Required(ErrorMessage ="{0} alanı gereklidir.")]
        // [DisplayFormat(ApplyFormatInEditMode=false,DataFormatString="{0:c}")]
        public decimal? Price { get; set; }


        [Display(Name ="Dosya Yolu")]
        public string ImagePath { get; set; }

        [Display(Name ="Ürün Fotoğrafları")]
        public List<Image> Images { get; set; } = new List<Image>(); // Ürüne resimler eklenmeden önce boş bir liste oluşturulması gerekir ki birden fazla resim dizi içerisine eklenebilsin.
        // Null reference hatası almamak için de gereklidir.


        [NotMapped]
        [Display(Name ="Dosya")]
        // [Required(ErrorMessage ="{0} alanı gereklidir.")]
        public IFormFile[] ImageFile { get; set; }
    }
}