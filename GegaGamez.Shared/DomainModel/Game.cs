﻿using System.ComponentModel.DataAnnotations;

namespace GegaGamez.Shared.DomainModel
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; } = null!;

        public DateTime? ReleaseDate { get; set; }

        [StringLength(1000)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(double.MinValue, double.MaxValue)]
        public decimal Price { get; set; }

        public Developer Developer { get; set; } = null!;

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public ICollection<DefaultCollectionType> DefaultCollections { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public ICollection<UserCollection> UserCollections { get; set; }
    }
}