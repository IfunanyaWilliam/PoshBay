﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Data.ViewModels
{
    public class ProductDetailViewModel
    {
        public string ProductId { get; set; } 

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }
        public string? ImagePath { get; set; }
        public string? CategoryId { get; set; }
    }
}
