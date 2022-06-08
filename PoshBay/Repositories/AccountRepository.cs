using Microsoft.EntityFrameworkCore;
using PoshBay.Contracts;
using PoshBay.Data;
using PoshBay.Data.Data;
using PoshBay.Data.Models;

namespace PoshBay.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public void Add(ApplicationUser user)
        {
            _context.ApplicationUsers.Add(user);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var user = _context.ApplicationUsers.Find(id);
            if (user is not null)
            {
                _context.ApplicationUsers.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers.OrderBy(u => u.FullName);
        }

        public ApplicationUser GetById(string id)
        {
            return _context.ApplicationUsers.Where(u => u.UserId == id).FirstOrDefault();
        }

        public async Task<bool> EmailExisAsync(string email)
        {
            var emailExist = _context.ApplicationUsers.FirstOrDefault(e => e.Email == email);
            if(emailExist is null)
            {
                return false;
            }
            return true;
        }
        public void Update(ApplicationUser user)
        {
            //for Optimistic Concurrency
            var userInDb = _context.Entry(user);
            userInDb.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
