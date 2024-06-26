﻿using NTTRUNG_Laze_Languages_Application.Dtos.Entity;
using NTTRUNG_Laze_Languages_Application.Interface.Base;
using NTTRUNG_Laze_Languages_Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Application.Interface.Service
{
    public interface IRoleService : ICRUDService< RoleModel, RoleDto>
    {
    }
}
