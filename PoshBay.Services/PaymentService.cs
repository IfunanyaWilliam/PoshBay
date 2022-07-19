using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PayStack.Net;
using PoshBay.Data.Data;
using PoshBay.Data.ViewModels;

namespace PoshBay.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PayStackConfiguration _payStack;

        public PaymentService(IOptions<PayStackConfiguration> payStack)
        {
            _payStack = payStack.Value;
            PayStack = new PayStackApi(_payStack.SecretKey);
        }      

        //This property will hold the PaystackApi class
        public PayStackApi PayStack { get; set; }

        public async Task<TransactionInitializeRequest> Request(TransactionViewModel transaction)
        {
            TransactionInitializeRequest request = new()
            {
                AmountInKobo = transaction.Amount * 100,
                Email        = transaction.Email,
                Reference    = RandomNumber().ToString(),
                Currency     = "NGN",
                CallbackUrl  = "https://localhost:7207/CheckOut/PaymentDetails"    //Return URL when payment processing is done from Paystack
            };

            return request;
        }

        public TransactionInitializeResponse Response(TransactionInitializeRequest request)
        {
            TransactionInitializeResponse response = PayStack.Transactions.Initialize(request);
            return response;
        }

        public async Task<TransactionVerifyResponse> VerifyPayment(string reference)
        {
            TransactionVerifyResponse verify = PayStack.Transactions.Verify(reference);
            return verify;
        }


        public static int RandomNumber()
        {
            Random random = new Random((int)DateTime.UtcNow.Ticks);
            return random.Next(100000000, 999999999);
        }
    }
}
