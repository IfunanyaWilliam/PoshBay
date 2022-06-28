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

        public string? UserId { get; set; }
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
