using System;
using System.Collections.Generic;
using System.Text;

namespace Bll.DTO
{
    public class LoginResponceDTO
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
    }
}
