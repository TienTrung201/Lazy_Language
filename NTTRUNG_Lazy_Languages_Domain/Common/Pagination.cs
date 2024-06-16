using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Domain.Common
{
    public class Pagination<T>
    {
        /// <summary>
        /// Tổng bản ghi
        /// </summary>
        /// CreatedBy: NTTrung (17/07/2023)
        public int TotalRecords { get; set; } = 0;
        /// <summary>
        /// Tổng số trang
        /// </summary>
        /// CreatedBy: NTTrung (17/07/2023)
        public int TotalPage { get; set; }
        /// <summary>
        /// Danh sách bản ghi
        /// </summary>
        /// CreatedBy: NTTrung (17/07/2023)
        public List<T> Data { get; set; }
        public Pagination(List<T> data, int totalRecords)
        {
            Data = data;
            TotalRecords = totalRecords;
            TotalPage = (int)Math.Ceiling(TotalRecords / (float)data.Count());
        }

    }
}
