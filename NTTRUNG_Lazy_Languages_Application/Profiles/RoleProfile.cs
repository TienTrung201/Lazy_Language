using AutoMapper;
using NTTRUNG_Laze_Languages_Application.Dtos.Entity;
using NTTRUNG_Laze_Languages_Domain.Entity;
using NTTRUNG_Laze_Languages_Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.NTTrungWeb05.GD2.Application.Profiles
{
    public class RoleProfile : Profile
    {
        /// <summary>
        /// Đăng ký Mapper
        /// </summary>
        /// CreatedBy: NTTrung (22/08/2023)
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleModel, RoleDto>();
            CreateMap<RoleDto, Role>();
            CreateMap<RoleModel, Role>();
        }
    }
}
