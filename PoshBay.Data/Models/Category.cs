using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.Models
{
    public class Category
    {

        [Key]
        public string CategoryId { get; set; } = Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "$"); 
        public string? Name { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; } = DateTime.Now;
        public string? CreatededBy { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; } = string.Empty;
    }
}
