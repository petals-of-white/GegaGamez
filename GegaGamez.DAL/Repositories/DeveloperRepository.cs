using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class DeveloperRepository : Repository<Developer>, IDeveloperRepository
{
    public DeveloperRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<Developer> GetActiveDevelopers() => GetAll(dev => dev.EndDate == null);

    public Task<IEnumerable<Developer>> GetActiveDevelopersAsync() => GetAllAsync(dev => dev.EndDate == null);

    public IEnumerable<Developer> GetAllByName(string name)
    {
        var devsByName = (from dev in DbSetWithIncludedProperties
                          where dev.Name.ToLower().Contains(name.ToLower())
                          select dev).ToList();

        return devsByName;
    }

    public async Task<IEnumerable<Developer>> GetAllByNameAsync(string name)
    {
        var devsByName = await (from dev in DbSetWithIncludedProperties
                                where dev.Name.ToLower().Contains(name.ToLower())
                                select dev).ToListAsync();

        return devsByName;
    }
}
