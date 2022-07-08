using Microsoft.EntityFrameworkCore;
using PoshBay.Contracts;
using PoshBay.Data.Data;
using PoshBay.Data.Models;
using System.Linq.Expressions;

namespace PoshBay.Repositories
{
    public class CheckOutRepository : ICheckOutRepository
    {
        private readonly AppDbContext _context;

        public CheckOutRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ShoppingCart> GetShoppingCartAsync(Expression<Func<ShoppingCart, bool>> predicate)
        {
            return await _context.ShoppingCarts.Include(c => c.CartItems).Include(a => a.AppUser).AsQueryable().FirstOrDefaultAsync(predicate);
        }
    }
}
