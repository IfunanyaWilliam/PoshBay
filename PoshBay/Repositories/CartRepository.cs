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

        public async Task<CartItem> GetCartAsync(string cartItemId)
        {
            return await _context.CartItem.FirstOrDefaultAsync(i => i.CartItemId == cartItemId);
        }

        public async Task<bool> AddCartAsync(CartItem cart)
        {
            _context.CartItem.Add(cart);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCartAsync(CartItem cart)
        {
            _context.CartItem.Update(cart);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(string AppUserId)
        {
            return await _context.ShoppingCarts.FirstOrDefaultAsync(i => i.AppUserId == AppUserId);
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

        public async Task<ShoppingCart> GetShoppingCartItemsAsync(string appUserId)
        {
            //CartItems is included b/c its a sub property of ShoppingCart. ThenInclude adds Product which is a sub property of CartItems
            return await _context.ShoppingCarts.Include(x => x.CartItems).ThenInclude(p => p.Product).FirstOrDefaultAsync(x => x.AppUser.Id == appUserId);
        }

        public async Task<bool> RemoveCartAsync(CartItem cart)
        {
            _context.CartItem.Remove(cart); 
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
