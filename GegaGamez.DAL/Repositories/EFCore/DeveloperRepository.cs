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
            // this anonymous comparer method might be replaced by something better in the future
            var titleSearcher = delegate (string inputTitle, string compareToTitle)
            {
                return compareToTitle.ToLower().Contains(inputTitle.ToLower());
            };

            var devsByName = (from dev in _dbSet
                              where titleSearcher(name, dev.Name)
                              select dev).ToList();

            return devsByName;
        }
    }
}
