using PoshBay.Data.Models;

namespace PoshBay.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id);
        Task<bool> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        void Delete(int id);

        IEnumerable<Category> GetAllCategory();
    }
}
