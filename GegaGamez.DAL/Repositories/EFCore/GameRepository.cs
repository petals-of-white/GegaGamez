﻿using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Game> GetAllByRatingScore(int score)
        {
            return _dbSet.Where(g => Math.Floor(g.Ratings.Average(r => r.RatingScore)) == score).ToList();
        }

        public async Task<IEnumerable<Game>> GetAllByRatingScoreAsync(int score)
        {
            return await _dbSet.Where(g => Math.Floor(g.Ratings.Average(r => r.RatingScore)) == score).ToListAsync();
        }

        public IEnumerable<Game> GetAllByTitle(string title)
        {
            var gamesByTitle = (from game in _dbSet
                                where game.Title.Contains(title, StringComparison.OrdinalIgnoreCase)
                                select game).ToList();

            return gamesByTitle;
        }

        public async Task<IEnumerable<Game>> GetAllByTitleAsync(string title)
        {
            var gamesByTitle = await (from game in _dbSet
                                      where game.Title.Contains(title, StringComparison.OrdinalIgnoreCase)
                                      select game).ToListAsync();

            return gamesByTitle;
        }

        public IEnumerable<Game> GetByGenre(Genre genre)
        {
            var gamesByGenre = (from game in _dbSet
                                where game.Genres.Select(g => g.Id).Contains(genre.Id)
                                select game).ToList();
            return gamesByGenre;
        }

        public async Task<IEnumerable<Game>> GetByGenreAsync(Genre genre)
        {
            var gamesByGenre = await (from game in _dbSet
                                      where game.Genres.Select(g => g.Id).Contains(genre.Id)
                                      select game).ToListAsync();
            return gamesByGenre;
        }
    }
}