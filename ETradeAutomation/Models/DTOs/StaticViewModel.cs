using System.Collections.Generic;
namespace CoreMvcOnlineTicariOtomasyon.Models.DTOs
{
    public class StaticViewModel
    {
        public IEnumerable<CurrentCityDto> CurrentCityDtos{get;set;}
        public IEnumerable<EmployeesDepartmentDto> EmployeesDepartmentDtos{get;set;}
        public IEnumerable<Current> Currents{get;set;}
        public IEnumerable<Product> Products{get;set;}
        public IEnumerable<SaleMovement> SaleMovements{get;set;}

        public IEnumerable<BrandsDto> BrandsDtos{get;set;}
    }
}