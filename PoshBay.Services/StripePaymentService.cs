using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace PoshBay.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly StripeConfiguration _stripeConfig;

        public StripePaymentService(IOptions<StripeConfiguration> stripeConfig)
        {
            _stripeConfig = stripeConfig.Value;
        }

        public async Task<bool> StripePayment(string stripeEmail, string stripeToken)
        {
            return false;
        }

    }
}
