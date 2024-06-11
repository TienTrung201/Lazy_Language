using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Application.Dtos.Entity.Account
{
    public class RegisterDto
    {
        public string? UserName { get; set; }
        public string? UserCode { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
    }
}
