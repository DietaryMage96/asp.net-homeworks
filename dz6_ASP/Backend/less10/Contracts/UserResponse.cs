namespace less10.DTOs
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        //public string Id { get; set; } = string.Empty;
        //public string Login { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
    }
}
