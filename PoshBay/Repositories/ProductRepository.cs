﻿using Microsoft.EntityFrameworkCore;
using PoshBay.Contracts;
using PoshBay.Data.Data;
using PoshBay.Data.Models;

namespace PoshBay.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        
        
        public async Task<bool> AddAsync(Product product)
        {
            _context.Products.Add(product);
            return await _context.SaveChangesAsync() > 0;
        }
        
        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }
        
        public async Task<bool> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync() 
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
