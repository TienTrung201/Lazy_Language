using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Application.Dtos.Entity.Account
{
    public class LoginDto
    {
        public string? UserCode { get; set; } = string.Empty;
        public string PassWord { get; set; }
        public string? Email { get; set; } = string.Empty;
    }
}
