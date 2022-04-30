using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class CountryRepository : Repository<Country>, ICountryRepository
{
    public CountryRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
