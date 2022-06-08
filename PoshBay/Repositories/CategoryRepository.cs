using Microsoft.EntityFrameworkCore;
using PoshBay.Contracts;
using PoshBay.Data;
using PoshBay.Data.Data;
using PoshBay.Data.Models;
using System.Linq;

namespace PoshBay.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Category category)
        {
            var categoryToDelete = _context.Categories.Find(category.CreatededBy);
            if (categoryToDelete != null)
            {
                _context.Categories.Remove(categoryToDelete);
               return await _context.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(false);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        }
        
        public async Task<Category> GetByIdAsync(string id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            //for optimistic concurrency
            //var entry = _context.Entry(category);
            //entry.State = EntityState.Modified;
            //return await _context.SaveChangesAsync() > 0;

            //Update the Category in the database;
            _context.Categories.Update(category);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteAsync(string id)
        {
            var category = _context.Categories.Find(id);
            if(category is not null)
            {
                _context.Remove(category);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
