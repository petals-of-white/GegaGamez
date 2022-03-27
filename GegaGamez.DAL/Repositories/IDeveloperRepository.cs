using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GegaGamez.DAL.Entities;

namespace GegaGamez.DAL.Repositories
{
    public interface IDeveloperRepository : IRepository<Developer>
    {
        IEnumerable<Developer> GetActiveDevelopers();

        Developer? GetByName(string name);
    }
}
