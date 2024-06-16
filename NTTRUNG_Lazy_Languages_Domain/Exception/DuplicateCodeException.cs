using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Domain
{
    /// <summary>
    /// Lớp tượng trưng cho ngoại lệ xảy ra khi gặp mã trùng lặp
    /// </summary>
    /// Created: NTTrung (16/07/2023)
    public class DuplicateCodeException : Exception
    {
        /// <summary>
        /// Mã lỗi cho ngoại lệ
        /// </summary>

        public int ErrorCode { get; set; }
        /// <summary>
        /// Constructor mặc định
        /// </summary>
        public DuplicateCodeException() { }
        /// <summary>
        /// Constructor với mã lỗi
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        public DuplicateCodeException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// Constructor với thông báo và mã lỗi
        /// </summary>
        /// <param name="message">Thông báo ngoại lệ</param>
        /// <param name="errorCode">Mã lỗi</param>
        public DuplicateCodeException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// Constructor với thông báo
        /// </summary>
        /// <param name="message">Thông báo ngoại lệ</param>
        public DuplicateCodeException(string message) : base(message)
        {

        }
    }
}
