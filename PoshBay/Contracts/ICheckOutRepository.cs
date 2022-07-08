using PoshBay.Data.Models;
using System.Linq.Expressions;

namespace PoshBay.Contracts
{
    public interface ICheckOutRepository
    {
        Task<ShoppingCart> GetShoppingCartAsync(Expression<Func<ShoppingCart, bool>> predicate);
    }
}
