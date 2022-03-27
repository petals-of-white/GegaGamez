using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GegaGamez.DAL.Entities;

namespace GegaGamez.DAL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetByUsername(string username);

        User? GetByCredentials(string username, string password);
    }
}
