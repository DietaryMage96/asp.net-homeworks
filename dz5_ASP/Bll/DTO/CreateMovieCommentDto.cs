using System;
using System.Collections.Generic;
using System.Text;

namespace Bll.DTO
{
    public class CreateMovieCommentDto
    {
        public int MovieId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}
