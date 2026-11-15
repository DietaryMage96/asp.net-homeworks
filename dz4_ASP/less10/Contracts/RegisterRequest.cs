using System.ComponentModel.DataAnnotations;

namespace less10.DTOs
{
    public class IdentityRegisterRequest
    {
        [Required(ErrorMessage = "Login is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be 3-50 characters")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Must be 6-20 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).+$", ErrorMessage = "Requires uppercase and lowercase letters")]
        public string Pass { get; set; } = string.Empty;

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("Pass", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be 3-50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be 3-50 characters")]
        public string Surname { get; set; } = string.Empty;

        [Range(0, 120, ErrorMessage = "Must be between 0 and 120")]
        public int Age { get; set; }
    }
}
