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
            // this anonymous comparer method might be replaced by something better in the future
            var titleSearcher = delegate (string inputTitle, string compareToTitle)
            {
                return compareToTitle.ToLower().Contains(inputTitle.ToLower());
            };

            var genresByName = (from genre in _dbSet
                                where titleSearcher(name, genre.Name)
                                select genre).ToList();

            return genresByName;
        }
    }
}
