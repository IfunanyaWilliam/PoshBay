using PoshBay.Data.Models;

namespace PoshBay.Contracts
{
    public interface ICartRepository
    {
        Task<bool> AddToCartAsync(string id);
        Task<IEnumerable<Cart>> GetCartItemsAsync();
    }
}
