using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.Models
{
    public class ShoppingCart
    {
        [Key]
        public string ShoppingCartId { get; private set; } = Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "$");
        public string? UserId { get; set; }
        public List<Cart>? CartItems { get; set; } = new List<Cart>();
    }
}
