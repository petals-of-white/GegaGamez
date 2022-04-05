using GegaGamez.BLL.Models;
using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;

namespace GegaGamez.BLL.Logic
{
    public class CollectionsBL
    {
        private readonly IUnitOfWork _db;

        public CollectionsBL(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        public IEnumerable<UserCollection> GetUserCollections(User user)
        {
            var userEntity = AutoMapping.Mapper.Map<DAL.Entities.User>(user);
            var uc = _db.UserCollections.GetAllByUser(userEntity);

            var output = AutoMapping.Mapper.Map<IEnumerable<UserCollection>>(uc);

            return output;
        }

        public void AddGameToUserCollection(UserCollection userCollection, Game game)
        {
            //var userCollectionEntity = AutoMapping.Mapper.Map<DAL.Entities.UserCollection>(userCollection);
            var gameEntity = AutoMapping.Mapper.Map<DAL.Entities.Game>(game);

            var userCollectionEntity = _db.UserCollections.Get(userCollection.Id);

            // !!! Something's not right here... Need to check where this is a good idea
            userCollectionEntity?.Games.Add(gameEntity);

            _db.Save();
        }

        public void AddGameToDefaultCollection(DefaultCollection defaultCollection, Game game)
        {
            //var userCollectionEntity = AutoMapping.Mapper.Map<DAL.Entities.UserCollection>(userCollection);
            var gameEntity = AutoMapping.Mapper.Map<DAL.Entities.Game>(game);

            var defaultCollectionEntity = _db.DefaultCollections.Get(defaultCollection.Id);

            // !!! Something's not right here... Need to check where this is a good idea
            defaultCollectionEntity.Add(gameEntity);

            _db.Save();
        }

        public IEnumerable<DefaultCollection> GetDefaultCollections(User user)
        {
            var userEntity = AutoMapping.Mapper.Map<DAL.Entities.User>(user);

            var dc = _db.DefaultCollections.GetByUser(userEntity);

            var output = AutoMapping.Mapper.Map<IEnumerable<DefaultCollection>>(dc);

            return output;
        }
    }
}
