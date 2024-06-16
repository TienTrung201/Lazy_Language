
using NTTRUNG_Lazy_Languages_Domain.Entity;
using NTTRUNG_Lazy_Languages_Domain.Interface.Base;
using NTTRUNG_Lazy_Languages_Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Domain.Interface.Repository
{
    public interface IRoleRepository : ICRUDRepository<Role, RoleModel>
    {
    }
}
