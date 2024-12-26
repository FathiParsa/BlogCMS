using DataAccessLayer.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EF.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }


        public async Task<User> FindByUsername(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}