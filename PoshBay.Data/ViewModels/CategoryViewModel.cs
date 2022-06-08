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
        public string CategoryId { get; set; }
        [Required]
        public string?  Name { get; set; }
        public string UserName { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
