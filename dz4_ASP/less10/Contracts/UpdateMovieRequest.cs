using System.ComponentModel.DataAnnotations;

namespace less10.Contracts
{
    public class UpdateMovieRequest
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Max length is 100")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Max length is 500")]
        public string Description { get; set; } = string.Empty;

        [Range(1888, 2030, ErrorMessage = "Must be between 1888 and 2030")]
        public int ReleaseYear { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Must be between 0 and 10")]
        public double Rating { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "PosterUrl is required")]
        public string PosterUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "FilmUrl is required")]
        public string FilmUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = string.Empty;
    }
}
