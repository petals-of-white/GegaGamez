﻿using GegaGamez.DAL.Repositories;

namespace GegaGamez.DAL.Services
{
    public interface IUnitOfWork : IDisposable
    {
        ICommentRepository Comments { get; }
        ICountryRepository Countries { get; }
        IDefaultCollectionRepository DefaultCollections { get; }
        IDefaultCollectionTypeRepository DefaultCollectionType { get; }
        IDeveloperRepository Developers { get; }
        IGameRepository Games { get; }
        IGenreRepository Genres { get; }
        IRatingRepository Ratings { get; }
        IUserRepository Users { get; }
        IUserCollectionRepository UsersCollections { get; }

        int Save();

        void Update<TEntity>(TEntity entityToUpdate) where TEntity : class;
    }
}