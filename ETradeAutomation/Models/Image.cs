namespace CoreMvcOnlineTicariOtomasyon.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CurrentId { get; set; }
        public Current Current { get; set; }


        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }





    }
}