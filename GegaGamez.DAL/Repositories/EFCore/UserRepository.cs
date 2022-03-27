using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public User? GetByCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }

        public User? GetByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
