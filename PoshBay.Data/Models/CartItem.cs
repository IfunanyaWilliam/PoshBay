using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.Models
{
    public class CartItem
    {
        [Key]
        public string CartId { get; private set; } = Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "$");
        public string? ProductId { get; set; }
        public int SelectedQuantity { get; set; }
        public Product? Product { get; set; }
        public decimal TotalPrice { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
