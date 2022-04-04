using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class UserModel
    {
        private readonly IUnitOfWork _db;
        private string _password;

        public UserModel(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        public int Id
        {
            get => default;
            set
            {
            }
        }

        public string Username
        {
            get => default;
            set
            {
            }
        }

        public int Name
        {
            get => default;
            set
            {
            }
        }

        public Country Country
        {
            get => default;
            set
            {
            }
        }

        public string About
        {
            get => default;
            set
            {
            }
        }

        public string Password
        {
            set
            {
                _password = value;
            }
        }

        public ICollection<DefaultCollection> DefaultCollections
        {
            get => default;
            set
            {
            }
        }

        public ICollection<UserCollection> UserCollections
        {
            get => default;
            set
            {
            }
        }

        public ICollection<Rating> Ratings
        {
            get => default;
            set
            {
            }
        }

        public void CreateUser()
        {
            // add this to the db
            throw new NotImplementedException();
        }

        public void CreateUserCollection()
        {
            throw new NotImplementedException();
        }

        public void AddGameToCollection()
        {
            throw new NotImplementedException();
        }

        public void RateGame(byte ratingScore)
        {
            throw new NotImplementedException();
        }

        public void LeaveComment(string commentText)
        {
            throw new NotImplementedException();
        }
    }
}
