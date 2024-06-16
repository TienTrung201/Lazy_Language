using NTTRUNG_Lazy_Languages_Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Domain.Entity
{
    /// <summary>
    /// Role
    /// </summary>
    /// CreatedBy NTTrung 21.04.24
    public class Role : BaseAudiEntity
    {
        /// <summary>
        /// Định danh
        /// </summary>
        public Guid RoleID { get; set; }
        public EnumRole? RoleType { get; set; }
        /// <summary>
        /// Tên user
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; } = string.Empty;
        /// <summary>
        /// Lấy Id Entity
        /// </summary>
        public override Guid GetKey()
        {
            return RoleID;
        }
    }
}
