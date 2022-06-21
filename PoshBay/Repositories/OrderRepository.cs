using PoshBay.Contracts;
using PoshBay.Data.Models;

namespace PoshBay.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public Task<bool> Create(OrderDetails oder)
        {
            return Task.FromResult(true);
        }

        public Task<bool> Delete(OrderDetails oder)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PaymentStatus(string orderDetalsId, string SessionId, string paymentIntentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(OrderDetails oder)
        {
            throw new NotImplementedException();
            
        }
    }
}
