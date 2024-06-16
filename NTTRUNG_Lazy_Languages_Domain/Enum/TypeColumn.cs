using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Domain.Enum
{
    /// <summary>
    /// Enum Loại cột dữ liệu
    /// </summary>
    /// Created by: NTTrung (13/07/2023)
    public enum TypeColumn
    {

        [Description("Giới tính")]
        Gender = 0,
        [Description("Ngày tháng năm")]
        Date = 1,
        [Description("Tiền")]
        Money = 2,
        [Description("Mặc định")]
        Default = 3,
        [Description("Hiển thị")]
        Show = 4,
        [Description("Trạng thái kinh doanh")]
        Business = 5,
        [Description("Mã")]
        Code = 6,
    }
    /// <summary>
    /// Enum căn trái phải giữa cột
    /// </summary>
    /// Created by: NTTrung (13/07/2023)
    public enum AlignColumn
    {

        [Description("Giữa")]
        Center = 0,
        [Description("Phải")]
        Right = 1,
        [Description("Trái")]
        Left = 2,
    }
}
