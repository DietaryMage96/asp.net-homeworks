namespace Domain.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
        public string Genre { get; set; }
        public string PosterUrl { get; set; } 
        public string FilmUrl { get; set; }
        public string Country { get; set; }
        public List<MovieComment> Comments { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
