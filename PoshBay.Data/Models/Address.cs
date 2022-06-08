using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.Models
{
    public class Address
    {

        [Key]
        public string AddressId { get; private set; } = Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "#");
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
    }
}
