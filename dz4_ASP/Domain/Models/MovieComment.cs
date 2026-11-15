using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MovieComment
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string AuthorName { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
