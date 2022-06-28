using PoshBay.Contracts;
using PoshBay.Data.Data;
using PoshBay.Data.Models;

namespace PoshBay.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<bool> AddToCartAsync(string id)
        {
            //retrieve the product from the database.
            var cart = _context.Carts.Find(id);

            var cartItem = _context.ShoppingCarts.FirstOrDefault(c => c.CartId == cart.CartId)
        }

        public Task<IEnumerable<Cart>> GetCartItemsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
