using PoshBay.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.ViewModels
{
    public class CategoryViewModel
    {
        [Required]
        public string?  Name { get; set; }
        public string? UserId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatededBy { get; set; }
    }
}
