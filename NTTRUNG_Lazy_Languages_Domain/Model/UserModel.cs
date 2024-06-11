using NTTRUNG_Laze_Languages_Domain.Entity;
using NTTRUNG_Laze_Languages_Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Domain.Model
{
    public class UserModel : User
    {
        public EnumRole? RoleType { get; set; }
        public string? RoleName { get; set; }
    }
}
