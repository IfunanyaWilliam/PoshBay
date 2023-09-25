using Microsoft.AspNetCore.Http;
using PoshBay.Data.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PoshBay.Data.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Product name is required")]
        public string? Name { get; set; }
        

        [Required(ErrorMessage = "Product description is required")]
        public string? Description { get; set; }

        
        [Required(ErrorMessage = "Please enter the price")]
        public decimal Price { get; set; }

        
        [Required(ErrorMessage = "Please enter the quantity of the product")]
        public int QuantityInStock { get; set; }

        [AllowedExtension(new string[] {".jpd", ".png", ".jpeg"})]
        [Required(ErrorMessage = "Please upload an image of the product")]
        public IFormFile? ImagePath { get; set; }
        

        [Required(ErrorMessage = "Please Select the category of the product")]
        [DisplayName("Category")]
        public string? CategoryId { get; set; }
    }
}
