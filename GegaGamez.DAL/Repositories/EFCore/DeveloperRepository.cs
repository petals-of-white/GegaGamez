using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class DeveloperRepository : Repository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Developer> GetActiveDevelopers() => GetAll(dev => dev.EndDate == null);

        public IEnumerable<Developer> GetAllByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
