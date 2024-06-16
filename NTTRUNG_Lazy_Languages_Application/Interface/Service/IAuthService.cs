using NTTRUNG_Lazy_Languages_Application.Dtos.Entity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Application.Interface.Service
{
    public interface IAuthService
    {
        Task<string> AuthenticateUser(LoginDto loginDto);
        Task<string> RegisterUser(RegisterDto registerDto);
    }
}
