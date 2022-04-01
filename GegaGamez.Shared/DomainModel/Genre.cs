﻿using System.ComponentModel.DataAnnotations;

namespace GegaGamez.Shared.DomainModel
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; } = null!;

        [StringLength(10)]
        [Required]
        public string Description { get; set; }

        /*
        [ForeignKey("GenreId")]
        [InverseProperty("Genres")]
        public virtual ICollection<Game> Games { get; set; }
        */
    }
}