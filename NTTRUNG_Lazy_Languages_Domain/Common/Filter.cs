using NTTRUNG_Lazy_Languages_Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Domain.Common
{
    public class FilterSort
    {
        public List<Filter>? Filter { get; set; }
        // <summary>
        // Tên bảng cần sắp xếp
        // </summary>
        // createdby: nttrung (22/08/2023)
        public string? PropertySort { get; set; } = string.Empty;
        // <summary>
        // Sắp xếp theo kiểu nào
        // </summary>
        // createdby: nttrung (22/08/2023)
        public SortBy? SortBy { get; set; }
        // <summary>
        // Loại cột đang được sắp xếp
        // </summary>
        // createdby: nttrung (22/08/2023)
        public TypeColumn? TypeColumn { get; set; }
        // <summary>
        // Trang hiện tại
        // </summary>
        // createdby: nttrung (22/08/2023)
        [Required]
        public int CurrentPage { get; set; }
        // <summary>
        // Số lượng bản ghi trong trang
        // </summary>
        // createdby: nttrung (22/08/2023)
        [Required]
        public int PageSize { get; set; }
    }
    public class Filter
    {
        // <summary>
        // Tên thuộc tính
        // </summary>
        // createdby: nttrung (22/08/2023)
        public string? Property { get; set; } = string.Empty;
        // <summary>
        // Giá trị
        // </summary>
        // createdby: nttrung (22/08/2023)
        public string? Value { get; set; } = string.Empty;
        // <summary>
        // Loại giữ liệu
        // </summary>
        // createdby: nttrung (22/08/2023)
        public PropertyType? PropertyType { get; set; }
        // <summary>
        // Kiểu so sánh
        // </summary>
        // createdby: nttrung (22/08/2023)
        public Operator? Operator { get; set; }
        // <summary>
        // Kiểu so sánh And Or
        // </summary>
        // createdby: nttrung (22/08/2023)
        public RelationType? RelationType { get; set; }
    }
}
