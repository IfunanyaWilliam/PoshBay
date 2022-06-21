using PoshBay.Data.Models;

namespace PoshBay.Contracts
{
    public interface IOrderRepository
    {
        Task<bool> Update(OrderDetails oder);
        Task<bool> Delete(OrderDetails oder);
        Task<bool> PaymentStatus(string orderDetalsId, string SessionId, string paymentIntentId);
        Task<bool> Create(OrderDetails oder);
    }
}
