using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;
using System.Linq.Expressions;

namespace PoshBay.Contracts
{
    public interface ICheckOutRepository
    {
        Task<ShoppingCart> GetShoppingCartAsync(Expression<Func<ShoppingCart, bool>> predicate);

        Task<bool> AddTransactionAsync(Transaction model);
        Task<Transaction> GetTransactionAsync(Expression<Func<Transaction, bool>> predicate);
        Task<bool> UpdateTransactionAsync(Transaction transaction);
    }
}
