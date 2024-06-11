using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Domain.Enum
{
    public enum EnumRole
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Người cho thuê")]
        Lessee = 2,
        [Description("Người thuê")]
        Tenant = 3,
    }
}
