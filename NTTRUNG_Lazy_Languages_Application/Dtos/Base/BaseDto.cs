using NTTRUNG_Laze_Languages_Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Application.Dtos
{
    public abstract class BaseDto
    {
        /// <summary>
        /// Chế độ chỉnh sửa
        /// </summary>  
        public EditMode EditMode { get; set; } = EditMode.None;
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
