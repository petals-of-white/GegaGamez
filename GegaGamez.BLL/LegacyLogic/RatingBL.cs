using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;
using GegaGamez.Shared.BusinessModels;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Services;
using GegaGamez.Shared.Validation;

namespace GegaGamez.BLL.LegacyLogic
{
    public class RatingBL : IDisposable
    {
        private readonly IUnitOfWork _db;

        public RatingBL(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        public void RateGame(Rating rating)
        {
            try
            {
                ValidationManager.Validate(rating);
            }
            catch (MultipleValidationsException)
            {
                throw;
            }

            var ratingEntity = AutoMapping.Mapper.Map<Shared.Entities.Rating>(rating);

            // check if user rating for a game already exists
            bool ratingExist = _db.Ratings.GetAll(
                r => r.UserId == ratingEntity.UserId
                && r.GameId == ratingEntity.GameId)
                .Count() == 1;

            if (ratingExist)
            {
                _db.Update(ratingEntity);
            }
            else
            {
                _db.Ratings.Add(ratingEntity);
            }

            _db.Save();
        }

        public void Unrate(Rating rating)
        {
            var ratingEntity = AutoMapping.Mapper.Map<Shared.Entities.Rating>(rating);

            _db.Ratings.Remove(ratingEntity);

            _db.Save();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
