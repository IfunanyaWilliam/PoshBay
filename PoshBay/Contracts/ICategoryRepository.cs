using PoshBay.Data.Models;

namespace PoshBay.Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetByIdAsync(string id);
        Task<bool> AddAsync(Category category);
        Task<bool> UpdateAsync(Category category);
        Task<bool> DeleteAsync(string categoryId);
    }
}
