using Microsoft.EntityFrameworkCore;
using PoshBay.Contracts;
using PoshBay.Data.Data;
using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;
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

        public async Task<bool> AddTransactionAsync(Transaction model)
        {
              _context.Transactions.Add(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Transaction> GetTransactionAsync (Expression<Func<Transaction, bool>> predicate)
        {
            return await _context.Transactions.AsQueryable().Include(a => a.AppUser).FirstOrDefaultAsync(predicate);
        }

    }
}