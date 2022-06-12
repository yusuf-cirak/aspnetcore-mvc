using System.ComponentModel.DataAnnotations;

namespace ETradeErkanHurnalıP2.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        [Required] // Veya migration oluşturulduktan sonra onDelete'i cascade olarak ayarlayabilirsin.
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}