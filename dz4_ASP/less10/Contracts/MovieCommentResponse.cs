namespace less10.Contracts
{
    public class MovieCommentResponse
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
