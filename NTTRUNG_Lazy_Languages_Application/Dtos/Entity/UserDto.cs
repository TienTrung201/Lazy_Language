using NTTRUNG_Laze_Languages_Domain.Entity;
using NTTRUNG_Laze_Languages_Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Application.Dtos.Entity
{
    public class UserDto : BaseDto
    {
        /// <summary>
        /// Định danh
        /// </summary>
        public Guid? UserID { get; set; }
        public Guid? AddressID { get; set; }
        public Guid? RoleID { get; set; }
        public string UserCode { get; set; }
        public string PassWord { get; set; }
        /// <summary>
        /// Tên user
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; } = string.Empty;
        public EnumRole? RoleType { get; set; }
        public string? RoleName { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023) 
        public DateTimeOffset? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023) 
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023) 
        public DateTimeOffset? ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023) 
        public string? ModifiedBy { get; set; }
        /// <summary>
        /// Lấy Id Entity
        /// </summary>
        public override Guid GetKey()
        {
            return (Guid)UserID;
        }
    }
}
