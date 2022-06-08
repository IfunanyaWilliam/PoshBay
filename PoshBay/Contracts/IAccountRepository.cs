using PoshBay.Data;
using PoshBay.Data.Models;

namespace PoshBay.Contracts
{
    public interface IAccountRepository
    {
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser GetById(string id);
        Task<bool> EmailExisAsync(string email);
        void Add(ApplicationUser user);
        void Update(ApplicationUser user);
        void Delete(string id);
    }
}
