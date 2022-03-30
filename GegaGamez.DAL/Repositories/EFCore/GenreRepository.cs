using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Genre> GetAllByName(string name)
        {
            var genresByName = (from genre in _dbSet
                                where genre.Name.Contains(name, StringComparison.OrdinalIgnoreCase)
                                select genre).ToList();
            return genresByName;
        }

        public async Task<IEnumerable<Genre>> GetAllByNameAsync(string name)
        {
            var genresByName = await (from genre in _dbSet
                                      where genre.Name.Contains(name, StringComparison.OrdinalIgnoreCase)
                                      select genre).ToListAsync();

            return genresByName;
        }
    }
}