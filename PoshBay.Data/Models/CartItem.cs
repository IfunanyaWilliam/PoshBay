using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? CartItemId { get; set; }
        public int SelectedQuantity { get; set; }
        public Product? Product { get; set; }
        public decimal TotalPrice { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
