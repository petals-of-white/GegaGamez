﻿using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class UserCollectionRepository : Repository<UserCollection>, IUserCollectionRepository
    {
        public UserCollectionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<UserCollection> GetAllByUser(User user) => GetAll(uc => uc.UserId == user.Id);
    }
}