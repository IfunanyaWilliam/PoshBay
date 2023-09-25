using PoshBay.Data.Extensions;
using System.ComponentModel.DataAnnotations;

namespace PoshBay.DTO
{
    public class ProductEditDTO
    {
        public string ProductId { get; set; }


        [Required(ErrorMessage = "Please enter the name of the product")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Please enter product description")]
        public string? Description { get; set; }


        [Required(ErrorMessage = "Please enter the price of the product")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Please enter the quantity of the product")]
        public int QuantityInStock { get; set; }

        public string? ImagePath { get; set; }

        //Get the new file to be uploaded
        [AllowedExtension(new string[] { ".jpd", ".png", ".jpeg" })]
        public IFormFile? NewImagePath { get; set; }
        public string? CategoryId { get; set; }
    }
}
