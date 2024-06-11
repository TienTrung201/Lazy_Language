using AutoMapper;
using NTTRUNG_Laze_Languages_Application.Dtos.Entity;
using NTTRUNG_Laze_Languages_Application.Interface.Base;
using NTTRUNG_Laze_Languages_Application.Interface.Service;
using NTTRUNG_Laze_Languages_Application.Service.Base;
using NTTRUNG_Laze_Languages_Domain.Interface.Repository;
using NTTRUNG_Laze_Languages_Domain.Common;
using NTTRUNG_Laze_Languages_Domain.Entity;
using NTTRUNG_Laze_Languages_Domain.Interface.Base;
using NTTRUNG_Laze_Languages_Domain.Interface.UnitOfWork;
using NTTRUNG_Laze_Languages_Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Application.Service
{
    public class UserService : CodeService<User, UserModel, UserDto>, IUserService
    {
        public UserService(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(userRepository, mapper, unitOfWork)
        {
        }
    }
}
