using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GegaGamez.DAL.Entities;

namespace GegaGamez.DAL.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        /// <summary>
        /// </summary>
        /// <param name="genre"></param>
        /// <returns>All the games of this genre</returns>
        IEnumerable<Game> GetByGenre(Genre genre);

        /// <summary>
        ///
        /// </summary> <param name="title"</param> <returns>>All the games that match the title</returns>
        IEnumerable<Game> GetAllByTitle(string title);

        /// <summary>
        /// </summary>
        /// <param name="score"></param>
        /// <returns>All the games with specified rating score</returns>
        IEnumerable<Game> GetAllByRatingScore(int score);
    }
}
