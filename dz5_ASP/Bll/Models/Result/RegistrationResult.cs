using System;
using System.Collections.Generic;
using System.Text;

namespace Bll.Models.Result
{
    public class RegistrationResult
    {
        public bool Succes { get; set; }
        public string? UserId { get; set; }
        public string[] Errors { get; set; }
    }
}
