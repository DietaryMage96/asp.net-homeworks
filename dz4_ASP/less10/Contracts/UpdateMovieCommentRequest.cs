using System.ComponentModel.DataAnnotations;

namespace less10.Contracts
{
    public class UpdateMovieCommentRequest
    {
        [Required(ErrorMessage = "Text is required")]
        [StringLength(1000, ErrorMessage = "Max length is 1000")]
        public string Text { get; set; } = string.Empty;
    }
}
