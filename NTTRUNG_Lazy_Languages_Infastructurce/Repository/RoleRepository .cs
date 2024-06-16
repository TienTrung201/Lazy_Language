using NTTRUNG_Lazy_Languages_Domain.Interface.Repository;
using NTTRUNG_Lazy_Languages_Domain.Entity;
using NTTRUNG_Lazy_Languages_Domain.Interface.UnitOfWork;
using NTTRUNG_Lazy_Languages_Domain.Model;
using NTTRUNG_Lazy_Languages_Infastructurce.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Infastructurce.Repository
{
    public class RoleRepository : CodeRepository<Role,RoleModel>, IRoleRepository
    {
        public RoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
