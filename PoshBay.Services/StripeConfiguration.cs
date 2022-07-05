﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Services
{
    //These properties will be bound to the Stripe section in appsettings.json file 
    //and will be used to configure the Stripe payment. 
    //A services will be created in Program.cs file to Map to the StripeConfiguration class.
    public class StripeConfiguration
    {
        public string? SecretKey { get; set; }
        public string? PublishabelKey { get; set; }
    }
}
