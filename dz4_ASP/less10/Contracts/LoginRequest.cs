using System.ComponentModel.DataAnnotations;

namespace less10.Contracts
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Login is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be 3-50 characters")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Must be 6-20 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).+$", ErrorMessage = "Requires uppercase and lowercase letters")]
        public string Pass { get; set; } = string.Empty;
    }
}
