using Bll.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public string FilmUrl { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public List<MovieCommentDto> Comments { get; set; } = new();
    }
}
