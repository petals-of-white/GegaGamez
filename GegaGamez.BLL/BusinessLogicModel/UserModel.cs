using System.ComponentModel.DataAnnotations;
using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class UserModel
    {
        private string _password;
        private readonly IUnitOfWork _db;

        public UserModel(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        public void CreateUser()
        {
            // add this to the db
            throw new NotImplementedException();
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

        public int Country
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

        public int DefaultCollections
        {
            get => default;
            set
            {
            }
        }

        public int UserCollections
        {
            get => default;
            set
            {
            }
        }

        public int Comments
        {
            get => default;
            set
            {
            }
        }

        public int Ratings
        {
            get => default;
            set
            {
            }
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
