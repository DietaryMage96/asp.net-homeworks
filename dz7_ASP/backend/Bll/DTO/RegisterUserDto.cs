using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class RegisterUserDto
    {
        public string Login { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
