using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class UserCollectionRepository : Repository<UserCollection>, IUserCollectionRepository
    {
        protected override IQueryable<UserCollection> DbSetWithIncludedProperties => _dbSet.Include(uc => uc.User);

        public UserCollectionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override UserCollection? Get(int id) => DbSetWithIncludedProperties.SingleOrDefault(uc => uc.Id == id);

        public IEnumerable<UserCollection> GetAllByUser(User user) => GetAll(uc => uc.UserId == user.Id);

        public Task<IEnumerable<UserCollection>> GetAllByUserAsync(User user) => GetAllAsync(uc => uc.UserId == user.Id);

        public override Task<UserCollection?> GetAsync(int id) => DbSetWithIncludedProperties.SingleOrDefaultAsync(uc => uc.Id == id);
    }
}
