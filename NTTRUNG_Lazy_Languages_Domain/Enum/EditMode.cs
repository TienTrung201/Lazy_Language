using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Domain.Enum
{
    /// <summary>
    /// Enum Eddit mode
    /// </summary>
    /// CreatedBy: NTTrung (23/08/2023)
    public enum EditMode
    {
        [Description("Không thay đổi")]
        None = 1,
        [Description("Thêm mới")]
        Create = 2,
        [Description("Update")]
        Update = 3,
        [Description("Xóa")]
        Delete = 4,
        [Description("Copy")]
        Copy = 5,
    }
}
