using System.ComponentModel.DataAnnotations;
using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class UserModel : IValidatableObject
    {
        private readonly IUnitOfWork _db;
        private string _password;

        public UserModel(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        [Key]
        public int Id { get; private set; }

        [Required]
        [StringLength(50)]
        public string Username
        {
            get => default;
            set
            {
            }
        }

        [StringLength(512)]
        [Required]
        public string Password
        {
            set
            {
                _password = value;
            }
        }

        [StringLength(100)]
        public string? Name
        {
            get => default;
            set
            {
            }
        }

        [StringLength(100)]
        public string? About
        {
            get => default;
            set
            {
            }
        }

        public Country? Country
        {
            get => default;
            set
            {
            }
        }

        [Required]
        public ICollection<DefaultCollection> DefaultCollections { get; set; }


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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(this, validationContext, validationResults, true);

            return validationResults;
        }
    }
}
