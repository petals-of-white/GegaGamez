using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Game> GetAllByRatingScore(int score)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Game> GetAllByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Game> GetByGenre(Genre genre)
        {
            throw new NotImplementedException();
        }
    }
}
