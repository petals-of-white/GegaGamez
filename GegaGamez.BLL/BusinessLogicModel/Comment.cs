using System.ComponentModel.DataAnnotations;
using GegaGamez.DAL.Services;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class Comment
    {
        private IUnitOfWork _db;

        public Comment(IUnitOfWork db)
        {
            _db = db;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 1000, MinimumLength = 1)]
        public string Text { get; set; }

        [Required]
        public UserModel User { get; set; }
    }
}
