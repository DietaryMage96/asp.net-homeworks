using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class UserDto
    {
        //public string Id { get; set; }
        //public string Login { get; set; } = string.Empty;
        //public string Name { get; set; } = string.Empty;
        //public string Surname { get; set; } = string.Empty;
        //public int Age { get; set; }

        public string Id { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
    }
}
