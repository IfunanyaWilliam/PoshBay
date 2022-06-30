using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PoshBay.Contracts;
using PoshBay.Data.Data;
using PoshBay.Data.Models;
using System.Security.Claims;

namespace PoshBay.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem> GetCartAsync(string productId)
        {
            return await _context.Carts.FirstOrDefaultAsync(i => i.Product.ProductId == productId);
        }

        public async Task<bool> AddCartAsync(CartItem cart)
        {
            _context.Carts.Add(cart);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCartAsync(CartItem cart)
        {
            _context.Carts.Update(cart);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(string shoppingCartId)
        {
            return await _context.ShoppingCarts.FirstOrDefaultAsync(i => i.ShoppingCartId == shoppingCartId);
        }

        public async Task<bool> AddShoppingCartAsync(ShoppingCart cart)
        {
            _context.ShoppingCarts.Add(cart);
            return await _context.SaveChangesAsync() > 0;
        }

        public async
        Task<bool> UpdateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Update(shoppingCart);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<CartItem> GetShoppingCartItemsAsync(string shoppingCartId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveCartAsync(string cartId)
        {
            //should also remove associated shoppingcart
            var cart = _context.Carts.Find(cartId);
            var cartItems = _context.ShoppingCarts.FirstOrDefault(x => x.ShoppingCartId == cartId);

            if(cart is not null)
            {
                _context.Carts.Remove(cart);
                _context.ShoppingCarts.Remove(cartItems);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
