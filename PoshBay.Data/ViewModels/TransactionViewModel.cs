using PoshBay.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.ViewModels
{
    public class TransactionViewModel
    {
        //public string? TransactionId { get; set; }
        public string? AppUserId { get; set; }
        public ApplicationUser? AppUser { get; set; }
        public int Amount { get; set; }
        public string? Email { get; set; }
        public string? TransactionRef { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
