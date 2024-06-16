using AutoMapper;
using NTTRUNG_Lazy_Languages_Application.Dtos.Entity;
using NTTRUNG_Lazy_Languages_Application.Service.Base;
using NTTRUNG_Lazy_Languages_Domain.Entity;
using NTTRUNG_Lazy_Languages_Domain.Interface.Repository;
using NTTRUNG_Lazy_Languages_Domain.Interface.UnitOfWork;
using NTTRUNG_Lazy_Languages_Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTTRUNG_Lazy_Languages_Application.Interface.Service;

namespace NTTRUNG_Lazy_Languages_Application.Service
{
    public class RoleService : CRUDService<Role, RoleModel, RoleDto>, IRoleService
    {
        public RoleService(IRoleRepository roleRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(roleRepository, mapper, unitOfWork)
        {
        }
    }
}
