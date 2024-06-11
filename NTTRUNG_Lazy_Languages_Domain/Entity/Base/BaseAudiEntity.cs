using NTTRUNG_Laze_Languages_Domain.Resources.ErrorMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Domain.Entity
{
    /// <summary>
    /// Lớp chứa các thuộc tính dùng để theo dõi thông tin người tạo và người sửa
    /// </summary>
    /// <CreatedBy>NTTrung (Ngày 16/07/2023)</CreatedBy>
    public abstract class BaseAudiEntity
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023) 
        public DateTimeOffset? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023) 
        [MaxLength(255, ErrorMessage = "Người tạo tối đa 255 ký tự")]
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023) 
        [MaxLength(255, ErrorMessage = "Người sửa tối đa 255 ký tự")]
        public DateTimeOffset? ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023) 
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Lấy tên id của đối tượng
        /// </summary>
        /// <returns>Tên Id</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        public abstract Guid GetKey();
        /// <summary>
        /// hàm set giá trị cho property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyName, object value)
        {
            this.GetType().GetProperty(propertyName)?.SetValue(this, value);
        }
    }
}
