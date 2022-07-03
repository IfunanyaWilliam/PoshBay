using PoshBay.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.ViewModels
{
    public class CartViewModel
    {
        public string? ShoppingCartId { get; set; }
        public string? AppUserId { get; set; }
        LinkedList<CartItem>? CartItems { get; set; }
    }
}
