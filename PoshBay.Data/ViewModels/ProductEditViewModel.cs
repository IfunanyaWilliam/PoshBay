using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.ViewModels
{
    public class ProductEditViewModel
    {
        public string ProductId { get; private set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string? ImagePath { get; set; }
        public string? CategoryId { get; set; }
    }
}
