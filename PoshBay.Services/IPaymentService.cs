using PayStack.Net;
using PoshBay.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Services
{
    public interface IPaymentService
    {
        Task<TransactionInitializeRequest> Request(TransactionViewModel transaction);
        TransactionInitializeResponse Response(TransactionInitializeRequest request);
        Task<TransactionVerifyResponse> VerifyPayment(string reference);
    }
}