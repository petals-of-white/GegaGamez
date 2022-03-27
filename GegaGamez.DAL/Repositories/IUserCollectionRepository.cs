﻿using GegaGamez.DAL.Entities;

namespace GegaGamez.DAL.Repositories
{
    public interface IUserCollectionRepository : IRepository<UserCollection>
    {
        IEnumerable<UserCollection> GetAllByUser(User user);
    }
}