using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Entities
{
    [Table("Developer")]
    [Index("Name", Name = "NIX_Developer_Name")]
    public partial class Developer
    {
        public Developer()
        {
            Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateTime BeginDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [InverseProperty("Developer")]
        public virtual ICollection<Game> Games { get; set; }
    }
}