using AutoMapper;
using Microsoft.Win32;
using NTTRUNG_Lazy_Languages_Application.Dtos.Entity;
using NTTRUNG_Lazy_Languages_Application.Dtos.Entity.Account;
using NTTRUNG_Lazy_Languages_Domain.Entity;
using NTTRUNG_Lazy_Languages_Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.NTTrungWeb05.GD2.Application.Profiles
{
    public class UserProfile : Profile
    {
        /// <summary>
        /// Đăng ký Mapper
        /// </summary>
        /// CreatedBy: NTTrung (22/08/2023)
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserModel, User>();
            CreateMap<RegisterDto, UserDto>();
        }
    }
}
