using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.Models
{
    public class Transaction
    {
        [Key]
        public string TransactionId { get; set; } = Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "&");
        public string? AppUserId { get; set; }
        public ApplicationUser? AppUser { get; set; }
        public int Amount { get; set; }
        public string? Email { get; set; }
        public string? TransactionRef { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
