using System.ComponentModel.DataAnnotations;

namespace less10.Contracts
{
    public class CreateMovieCommentRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "MovieId must be greater than 0")]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "AuthorName is required")]
        [StringLength(100, ErrorMessage = "Max length is 100")]
        public string AuthorName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Text is required")]
        [StringLength(1000, ErrorMessage = "Max length is 1000")]
        public string Text { get; set; } = string.Empty;
    }
}
