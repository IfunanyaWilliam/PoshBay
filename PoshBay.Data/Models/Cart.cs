using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.Models
{
    public class Cart
    {

        [Key]
        public string CartId { get; private set; } = Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "$");
        public int SelectedQuantity { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Product? Product { get; set; }
        public DateTime? AddedOn { get; set; } = DateTime.Now;
    }
}
