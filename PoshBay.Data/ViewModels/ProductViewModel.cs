using PoshBay.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }
        

        [Required(ErrorMessage = "Product description is required")]
        public string Description { get; set; }

        
        [Required(ErrorMessage = "Please enter the price")]
        public decimal Price { get; set; }

        
        [Required(ErrorMessage = "Please enter the quantity of the product")]
        public int QuantityInStock { get; set; }

        
        [Required(ErrorMessage = "Please upload an image of the product")]
        public string ImagePath { get; set; }
        

        [Required(ErrorMessage = "Please enter the category of the product")]
        public Category Category { get; set; }
    }
}
