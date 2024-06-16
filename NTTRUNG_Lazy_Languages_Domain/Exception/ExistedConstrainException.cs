using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Domain
{
    public class ExistedConstrainException : Exception
    {
        /// <summary>
        /// Mã lỗi cho ngoại lệ
        /// </summary>
        /// createBy NTTrung (20/07/2023)
        public int ErrorCode { get; set; }
        /// <summary>
        /// Constructor mặc định
        /// </summary>
        /// createBy NTTrung (20/07/2023)
        public ExistedConstrainException() { }
        /// <summary>
        /// Constructor với mã lỗi
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        /// createBy NTTrung (20/07/2023)
        public ExistedConstrainException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// Constructor với thông báo và mã lỗi
        /// </summary>
        /// <param name="message">Thông báo ngoại lệ</param>
        /// <param name="errorCode">Mã lỗi</param>
        /// createBy NTTrung (20/07/2023)
        public ExistedConstrainException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// Constructor với thông báo
        /// </summary>
        /// <param name="message">Thông báo ngoại lệ</param>
        /// createBy NTTrung (20/07/2023)
        public ExistedConstrainException(string message) : base(message)
        {

        }

    }
}
