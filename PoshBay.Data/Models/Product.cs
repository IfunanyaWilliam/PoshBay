using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.Models
{
    public class Product
    {

        [Key]
        public string ProductId { get; private set; } = Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "#");
        
        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string ImagePath { get; set; }
        public Category Category { get; set; }
    }
}
