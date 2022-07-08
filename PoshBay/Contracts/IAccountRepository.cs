using PoshBay.Data;
using PoshBay.Data.Models;
using System.Linq.Expressions;

namespace PoshBay.Contracts
{
    public interface IAccountRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();

        //Espression three 
        Task<ApplicationUser> GetAppUser(Expression<Func<ApplicationUser, bool>> predicate);

        Task<bool> EmailExisAsync(string email);
        void Add(ApplicationUser user);
        void Update(ApplicationUser user);
        void Delete(string id);
    }
}
