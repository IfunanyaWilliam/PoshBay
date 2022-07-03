using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.Models
{
    public class OrderDetails
    {
        public string? OrderDetailId { get; set; }
        public string? AppUserId { get; set; }
        public ApplicationUser? AppUser { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? OrderTotal { get; set; }
        public string? PaymentStatus { get; set; }   
        public string? TrackingNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
