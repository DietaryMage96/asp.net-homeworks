using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        //public int Id { get; set; }
        //public string Login {  get; set; }
        //public string Pass {  get; set; }
        //public string Name {  get; set; }
        //public string Surname {  get; set; }
        //public int Age {  get; set; }

        public string DisplayName { get; set; }
        public string PhotoPath { get; set; }
        public List<Movie> Movies { get; set; }

    }
}
