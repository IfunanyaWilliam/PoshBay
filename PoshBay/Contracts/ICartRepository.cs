using PoshBay.Data.Models;
using System.Linq.Expressions;

namespace PoshBay.Contracts
{
    public interface ICartRepository
    {
        Task<CartItem> GetCartAsync(Expression<Func<CartItem, bool>> predicate);
        Task<bool> AddCartAsync(CartItem cart);
        Task<bool> UpdateCartAsync(CartItem cart);
        Task<bool> RemoveCartAsync(CartItem cart);
        Task<bool> AddShoppingCartAsync(ShoppingCart cart);
        Task<bool> UpdateShoppingCartAsync(ShoppingCart cart);

        Task<ShoppingCart> GetShoppingCartItemsAsync(string AppUserId);
    }
}
