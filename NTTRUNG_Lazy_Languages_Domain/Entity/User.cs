using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Domain.Entity
{
    /// <summary>
    /// User
    /// </summary>
    /// CreatedBy NTTrung 21.04.24
    public class User : BaseAudiEntity
    {
        /// <summary>
        /// Định danh
        /// </summary>
        public Guid UserID { get; set; }
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
        /// <summary>
        /// Lấy Id Entity
        /// </summary>
        public override Guid GetKey()
        {
            return UserID;
        }
    }
}
