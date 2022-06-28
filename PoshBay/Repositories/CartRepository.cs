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
            var prod = _context.Products.Find(id);

        }

        public Task<IEnumerable<Cart>> GetCartItemsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
