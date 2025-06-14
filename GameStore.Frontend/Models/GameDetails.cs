﻿using System.ComponentModel.DataAnnotations;

namespace GameStore.Frontend.Models
{
    public class GameDetails
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters long.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "The Genre field is required")] 
        public string? GenreId { get; set; }

        [Range(1, 100, ErrorMessage = "Price must be between 1 and 100.")]
        public decimal Price { get; set; }
        public DateOnly ReleaseDate { get; set; }
    }
}
