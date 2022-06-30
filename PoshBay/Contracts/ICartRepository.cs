using PoshBay.Data.Models;

namespace PoshBay.Contracts
{
    public interface ICartRepository
    {
        Task<CartItem> GetCartAsync(string productId);
        Task<bool> AddCartAsync(CartItem cart);
        Task<bool> UpdateCartAsync(CartItem cart);
        Task<bool> RemoveCartAsync(string cartId);
        Task<bool> AddShoppingCartAsync(ShoppingCart cart);
        Task<bool> UpdateShoppingCartAsync(ShoppingCart cart);

        Task<ShoppingCart> GetShoppingCartAsync(string shoppingCartId);
        Task<CartItem> GetShoppingCartItemsAsync(string shoppingCartId);
    }
}
