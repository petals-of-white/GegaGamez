using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
    public GenreRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
