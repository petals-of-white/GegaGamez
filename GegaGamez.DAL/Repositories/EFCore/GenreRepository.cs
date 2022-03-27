using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Genre? GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
