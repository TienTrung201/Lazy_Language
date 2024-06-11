using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Domain.Enum
{
    /// <summary>
    /// Enum mã lỗi
    /// </summary>
    /// CreatedBy: NTTrung (11/07/2023)
    public enum ErrorCode
    {
        [Description("Không có quyền truy cập")]
        Unauthorized = 1007,
        [Description("Đăng nhập không thành công")]
        LoginError = 1006,
        [Description("Đăng ký không thành công")]
        RegisterError = 1005,
        [Description("File không hợp lệ")]
        FIleImageInvalid = 1004,
        [Description("Có phát sinh")]
        ExistedConstrain = 1003,
        [Description("Trùng mã")]
        DuplicateCode = 1002,
        [Description("Trùng mã detail")]
        DuplicateCodeDetail = 1005,
        [Description("Không tìm thấy")]
        NotFoundCode = 1001,
        [Description("Lỗi server")]
        ErrorServerCode = 1000,

    }
}
