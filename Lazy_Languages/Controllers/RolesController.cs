using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using NTTRUNG_Lazy_Languages_Application.Service;
using NTTRUNG_Lazy_Languages_Application.Interface.Service;
using NTTRUNG_Lazy_Languages_Controllers.Base;
using NTTRUNG_Lazy_Languages_Domain.Entity;
using NTTRUNG_Lazy_Languages_Domain.Model;
using NTTRUNG_Lazy_Languages_Application.Dtos.Entity;
namespace NTTRUNG_Lazy_Languages_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : CRUDController<RoleModel, RoleDto>
    {
        public RolesController(IRoleService roleService) : base(roleService)
        {
        }
    }
}
