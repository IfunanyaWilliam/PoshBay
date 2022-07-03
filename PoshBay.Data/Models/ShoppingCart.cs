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
        public string ShoppingCartId { get; set; } = Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "#");
        public string? AppUserId { get; set; }
        public ApplicationUser? AppUser {get; set;}
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
