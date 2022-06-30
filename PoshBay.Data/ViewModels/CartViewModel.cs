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
        public string? CartId { get; private set; }
        public string? ProductId { get; set; }
        public int SelectedQuantity { get; set; }
        public Product? Product { get; set; }
        public decimal TotalPrice { get; set; }
        public string? AppUserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
