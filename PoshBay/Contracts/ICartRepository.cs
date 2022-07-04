using PoshBay.Data.Models;

namespace PoshBay.Contracts
{
    public interface ICartRepository
    {
        Task<CartItem> GetCartAsync(string cartItemId);
        Task<bool> AddCartAsync(CartItem cart);
        Task<bool> UpdateCartAsync(CartItem cart);
        Task<bool> RemoveCartAsync(CartItem cart);
        Task<bool> AddShoppingCartAsync(ShoppingCart cart);
        Task<bool> UpdateShoppingCartAsync(ShoppingCart cart);

        Task<ShoppingCart> GetShoppingCartAsync(string AppUserId);
        Task<ShoppingCart> GetShoppingCartItemsAsync(string AppUserId);
    }
}
