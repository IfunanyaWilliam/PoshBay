using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PoshBay.Data.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Key]
        public string AppUserId { get; private set; } = Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "&");
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public override string? Email { get; set; }

        public override string? PhoneNumber { get; set; } 
        public bool IsAdmin { get; set; } = false;
    }
}
